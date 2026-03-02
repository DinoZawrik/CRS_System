from fastapi import APIRouter, Request, Depends, HTTPException
from fastapi.responses import RedirectResponse
from fastapi.templating import Jinja2Templates
from sqlalchemy.orm import Session

from database import get_db
from models import Flight, Booking
from auth import get_current_user

router = APIRouter()
templates = Jinja2Templates(directory="templates")

TOTAL_SEATS = 12


@router.get("/flights")
async def flights_list(request: Request, city: str = "", db: Session = Depends(get_db)):
    try:
        current_user = get_current_user(request)
    except HTTPException:
        return RedirectResponse("/login", status_code=302)

    query = db.query(Flight)
    if city:
        query = query.filter(
            (Flight.departure_city.ilike(f"%{city}%")) |
            (Flight.destination_city.ilike(f"%{city}%"))
        )
    flights = query.all()

    # Determine which seats are taken per flight
    booked_seats: dict[str, list[str]] = {}
    for flight in flights:
        taken = [
            b.seat_number
            for b in db.query(Booking).filter(Booking.flight_number == flight.flight_number).all()
        ]
        booked_seats[flight.flight_number] = taken

    return templates.TemplateResponse(
        "flights.html",
        {
            "request": request,
            "flights": flights,
            "booked_seats": booked_seats,
            "city": city,
            "user": current_user,
        },
    )


@router.get("/book/{flight_number}")
async def book_get(flight_number: str, request: Request, db: Session = Depends(get_db)):
    try:
        current_user = get_current_user(request)
    except HTTPException:
        return RedirectResponse("/login", status_code=302)

    flight = db.query(Flight).filter(Flight.flight_number == flight_number).first()
    if not flight:
        return RedirectResponse("/flights", status_code=302)

    taken = [
        b.seat_number
        for b in db.query(Booking).filter(Booking.flight_number == flight_number).all()
    ]

    if len(taken) >= TOTAL_SEATS:
        return RedirectResponse("/flights", status_code=302)

    return templates.TemplateResponse(
        "book.html",
        {
            "request": request,
            "flight": flight,
            "taken_seats": taken,
            "user": current_user,
        },
    )
