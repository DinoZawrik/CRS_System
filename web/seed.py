"""Populate the database with 10 sample flights."""
from datetime import datetime, timedelta
from database import engine, SessionLocal, Base
from models import Flight

Base.metadata.create_all(bind=engine)

FLIGHTS = [
    ("SU101", "Moscow",       "Saint Petersburg", 2,  0,  89.00),
    ("SU202", "Moscow",       "Novosibirsk",      4, 30, 149.00),
    ("SU303", "Saint Petersburg", "Sochi",        2, 45, 129.00),
    ("SU404", "Moscow",       "Yekaterinburg",    2, 20, 109.00),
    ("SU505", "Novosibirsk",  "Vladivostok",      5, 10, 219.00),
    ("SU606", "Moscow",       "Kazan",            1, 30,  69.00),
    ("SU707", "Sochi",        "Moscow",           2, 50, 119.00),
    ("SU808", "Yekaterinburg","Saint Petersburg",  2, 55, 139.00),
    ("SU909", "Kazan",        "Novosibirsk",      3, 20, 159.00),
    ("SU010", "Moscow",       "Vladivostok",      8,  0, 299.00),
]

base_time = datetime(2026, 6, 1, 8, 0)

db = SessionLocal()

try:
    if db.query(Flight).count() == 0:
        for i, (fn, dep, dst, dur_h, dur_m, price) in enumerate(FLIGHTS):
            departure = base_time + timedelta(days=i, hours=i % 5)
            arrival = departure + timedelta(hours=dur_h, minutes=dur_m)
            db.add(Flight(
                flight_number=fn,
                departure_city=dep,
                destination_city=dst,
                departure_time=departure,
                arrival_time=arrival,
                price=price,
            ))
        db.commit()
        print(f"Seeded {len(FLIGHTS)} flights.")
    else:
        print("Flights already seeded — skipping.")
finally:
    db.close()
