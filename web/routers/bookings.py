from fastapi import APIRouter, Request, Form, Depends, HTTPException
from fastapi.responses import RedirectResponse
from fastapi.templating import Jinja2Templates
from sqlalchemy.orm import Session, joinedload

from database import get_db
from models import Flight, Booking
from auth import get_current_user

router = APIRouter()
templates = Jinja2Templates(directory="templates")


@router.post("/book/{flight_number}")
async def book_post(
    flight_number: str,
    request: Request,
    seat_number: str = Form(...),
    passenger_first_name: str = Form(...),
    passenger_last_name: str = Form(...),
    address: str = Form(...),
    passport_number: str = Form(...),
    db: Session = Depends(get_db),
):
    try:
        current_user = get_current_user(request)
    except HTTPException:
        return RedirectResponse("/login", status_code=302)

    flight = db.query(Flight).filter(Flight.flight_number == flight_number).first()
    if not flight:
        return RedirectResponse("/flights", status_code=302)

    # Check seat not already taken
    existing = (
        db.query(Booking)
        .filter(Booking.flight_number == flight_number, Booking.seat_number == seat_number)
        .first()
    )
    if existing:
        taken = [
            b.seat_number
            for b in db.query(Booking).filter(Booking.flight_number == flight_number).all()
        ]
        return templates.TemplateResponse(
            "book.html",
            {
                "request": request,
                "flight": flight,
                "taken_seats": taken,
                "user": current_user,
                "error": f"Seat {seat_number} is already taken. Please choose another.",
            },
        )

    booking = Booking(
        user_id=current_user["id"],
        flight_number=flight_number,
        seat_number=seat_number,
        passenger_first_name=passenger_first_name,
        passenger_last_name=passenger_last_name,
        address=address,
        passport_number=passport_number,
    )
    db.add(booking)
    db.commit()

    request.session["flash"] = f"Booking confirmed! Seat {seat_number} on flight {flight_number}."
    return RedirectResponse("/bookings", status_code=303)


@router.get("/bookings")
async def bookings_list(request: Request, db: Session = Depends(get_db)):
    try:
        current_user = get_current_user(request)
    except HTTPException:
        return RedirectResponse("/login", status_code=302)

    bookings = (
        db.query(Booking)
        .options(joinedload(Booking.flight))
        .filter(Booking.user_id == current_user["id"])
        .all()
    )

    flash = request.session.pop("flash", None)

    return templates.TemplateResponse(
        "bookings.html",
        {
            "request": request,
            "bookings": bookings,
            "user": current_user,
            "flash": flash,
        },
    )


@router.post("/bookings/{booking_id}/cancel")
async def cancel_booking(booking_id: int, request: Request, db: Session = Depends(get_db)):
    try:
        current_user = get_current_user(request)
    except HTTPException:
        return RedirectResponse("/login", status_code=302)

    booking = (
        db.query(Booking)
        .filter(Booking.id == booking_id, Booking.user_id == current_user["id"])
        .first()
    )
    if booking:
        db.delete(booking)
        db.commit()
        request.session["flash"] = "Booking cancelled successfully."

    return RedirectResponse("/bookings", status_code=303)
