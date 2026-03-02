from datetime import datetime
from decimal import Decimal
from sqlalchemy import String, Integer, ForeignKey, DateTime, Numeric, UniqueConstraint, func
from sqlalchemy.orm import Mapped, mapped_column, relationship
from database import Base


class User(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(Integer, primary_key=True, autoincrement=True)
    first_name: Mapped[str] = mapped_column(String(50))
    last_name: Mapped[str] = mapped_column(String(50))
    email: Mapped[str] = mapped_column(String(100), unique=True, index=True)
    hashed_password: Mapped[str] = mapped_column(String(255))
    created_at: Mapped[datetime] = mapped_column(DateTime, server_default=func.now())

    bookings: Mapped[list["Booking"]] = relationship("Booking", back_populates="user")


class Flight(Base):
    __tablename__ = "flights"

    flight_number: Mapped[str] = mapped_column(String(10), primary_key=True)
    departure_city: Mapped[str] = mapped_column(String(50))
    destination_city: Mapped[str] = mapped_column(String(50))
    departure_time: Mapped[datetime] = mapped_column(DateTime)
    arrival_time: Mapped[datetime] = mapped_column(DateTime)
    price: Mapped[Decimal] = mapped_column(Numeric(10, 2))

    bookings: Mapped[list["Booking"]] = relationship("Booking", back_populates="flight")


class Booking(Base):
    __tablename__ = "bookings"
    __table_args__ = (
        UniqueConstraint("flight_number", "seat_number", name="uq_booking_seat"),
    )

    id: Mapped[int] = mapped_column(Integer, primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column(Integer, ForeignKey("users.id"), nullable=False)
    flight_number: Mapped[str] = mapped_column(String(10), ForeignKey("flights.flight_number"), nullable=False)
    seat_number: Mapped[str] = mapped_column(String(10))
    passenger_first_name: Mapped[str] = mapped_column(String(50))
    passenger_last_name: Mapped[str] = mapped_column(String(50))
    address: Mapped[str] = mapped_column(String(150))
    passport_number: Mapped[str] = mapped_column(String(50))
    created_at: Mapped[datetime] = mapped_column(DateTime, server_default=func.now())

    user: Mapped["User"] = relationship("User", back_populates="bookings")
    flight: Mapped["Flight"] = relationship("Flight", back_populates="bookings")
