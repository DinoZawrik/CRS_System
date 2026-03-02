from fastapi import APIRouter, Request, Form, Depends
from fastapi.responses import RedirectResponse
from fastapi.templating import Jinja2Templates
from sqlalchemy.orm import Session

from database import get_db
from models import User
from auth import hash_password, verify_password, login_user, logout_user, redirect_if_authenticated

router = APIRouter()
templates = Jinja2Templates(directory="templates")


@router.get("/register")
async def register_get(request: Request):
    redir = redirect_if_authenticated(request)
    if redir:
        return redir
    return templates.TemplateResponse("register.html", {"request": request})


@router.post("/register")
async def register_post(
    request: Request,
    first_name: str = Form(...),
    last_name: str = Form(...),
    email: str = Form(...),
    password: str = Form(...),
    db: Session = Depends(get_db),
):
    existing = db.query(User).filter(User.email == email).first()
    if existing:
        return templates.TemplateResponse(
            "register.html",
            {"request": request, "error": "Email already registered."},
        )
    user = User(
        first_name=first_name,
        last_name=last_name,
        email=email,
        hashed_password=hash_password(password),
    )
    db.add(user)
    db.commit()
    db.refresh(user)
    login_user(request, user.id, f"{user.first_name} {user.last_name}")
    return RedirectResponse("/flights", status_code=303)


@router.get("/login")
async def login_get(request: Request):
    redir = redirect_if_authenticated(request)
    if redir:
        return redir
    return templates.TemplateResponse("login.html", {"request": request})


@router.post("/login")
async def login_post(
    request: Request,
    email: str = Form(...),
    password: str = Form(...),
    db: Session = Depends(get_db),
):
    user = db.query(User).filter(User.email == email).first()
    if not user or not verify_password(password, user.hashed_password):
        return templates.TemplateResponse(
            "login.html",
            {"request": request, "error": "Invalid email or password."},
        )
    login_user(request, user.id, f"{user.first_name} {user.last_name}")
    return RedirectResponse("/flights", status_code=303)


@router.post("/logout")
async def logout(request: Request):
    logout_user(request)
    return RedirectResponse("/login", status_code=303)
