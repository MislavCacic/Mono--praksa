CREATE TABLE "User" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Surname" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "PhoneNumber" INT
);

CREATE TABLE "Service" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Price" INT NOT NULL,
    "Description" TEXT
);

CREATE TABLE "Worker" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Surname" VARCHAR(255) NOT NULL,
    "BestAt" TEXT,
    "PhoneNumber" INT
);

CREATE TABLE "Reservation" (
    "Id" SERIAL PRIMARY KEY,
    "Time" TIMESTAMP NOT NULL,
    "IsActive" BOOLEAN,
    "UserId" INT NOT NULL,
    "WorkerId" INT NOT NULL,
    CONSTRAINT "FK_Reservation_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User"("Id"),
    CONSTRAINT "FK_Reservation_Worker_WorkerId" FOREIGN KEY ("WorkerId") REFERENCES "Worker"("Id")
);

CREATE TABLE "ReservationService" (
    "Id" SERIAL PRIMARY KEY,
    "Quantity" INT NOT NULL,
    "ReservationId" INT NOT NULL,
    "ServiceId" INT NOT NULL,
    CONSTRAINT "FK_ReservationService_Reservation_ReservationId" FOREIGN KEY ("ReservationId") REFERENCES "Reservation"("Id"),
    CONSTRAINT "FK_ReservationService_Service_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES "Service"("Id")
);

CREATE TABLE "Bill" (
    "Id" SERIAL PRIMARY KEY,
    "Total" DECIMAL(4, 2),
    "Date" TIMESTAMP,
    "ReservationId" INT NOT NULL,
    CONSTRAINT "FK_Bill_Reservation_ReservationId" FOREIGN KEY ("ReservationId") REFERENCES "Reservation"("Id")
);