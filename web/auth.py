import bcrypt
from fastapi import Request, HTTPException
from fastapi.responses import RedirectResponse


def hash_password(password: str) -> str:
    return bcrypt.hashpw(password.encode(), bcrypt.gensalt()).decode()


def verify_password(password: str, hashed: str) -> bool:
    return bcrypt.checkpw(password.encode(), hashed.encode())


def get_current_user(request: Request) -> dict:
    user = request.session.get("user")
    if not user:
        raise HTTPException(status_code=302, headers={"Location": "/login"})
    return user


def login_user(request: Request, user_id: int, user_name: str) -> None:
    request.session["user"] = {"id": user_id, "name": user_name}


def logout_user(request: Request) -> None:
    request.session.clear()


def redirect_if_authenticated(request: Request):
    if request.session.get("user"):
        return RedirectResponse("/flights", status_code=302)
    return None
