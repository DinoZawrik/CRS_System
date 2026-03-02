from fastapi import FastAPI, Request
from fastapi.staticfiles import StaticFiles
from fastapi.templating import Jinja2Templates
from starlette.middleware.sessions import SessionMiddleware

import os

from database import engine, Base
from routers import auth, flights, bookings

Base.metadata.create_all(bind=engine)

app = FastAPI(title="CRS — Central Reservation System")

app.add_middleware(
    SessionMiddleware,
    secret_key=os.environ.get("SECRET_KEY", "change-me-in-production"),
    same_site="lax",
    https_only=os.environ.get("HTTPS_ONLY", "false").lower() == "true",
)

app.mount("/static", StaticFiles(directory="static"), name="static")

templates = Jinja2Templates(directory="templates")

app.include_router(auth.router)
app.include_router(flights.router)
app.include_router(bookings.router)


@app.get("/")
async def home(request: Request):
    return templates.TemplateResponse("home.html", {"request": request})
