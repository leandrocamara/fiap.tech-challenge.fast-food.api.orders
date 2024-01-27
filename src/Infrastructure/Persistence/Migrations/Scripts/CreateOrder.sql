CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "orders"
(
    "Id"    UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "Cpf"   VARCHAR(11) NOT NULL,
    "Name"  VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL
);

CREATE TABLE "ordersItem"
(
    
);