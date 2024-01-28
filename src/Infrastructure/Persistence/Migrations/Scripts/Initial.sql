CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "customers"
(
    "Id"    UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "Cpf"   VARCHAR(11)  NOT NULL,
    "Name"  VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL
);

CREATE TABLE "products"
(
    "Id"          UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "Name"        VARCHAR(100)   NOT NULL,
    "Category"    SMALLINT       NOT NULL,
    "Price"       NUMERIC(10, 2) NOT NULL,
    "Description" VARCHAR(200)   NOT NULL,
    "Images"      JSONB
);

CREATE TABLE "orders"
(
    "Id"         UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "CustomerId" UUID NULL,
    "Status"     SMALLINT    NOT NULL,
    "CreatedAt"  TIMESTAMPTZ NOT NULL    DEFAULT NOW(),
    "TotalPrice" DECIMAL,
    FOREIGN KEY ("CustomerId") REFERENCES "customers" ("Id")
);

CREATE TABLE "orderItems"
(
    "Id"         UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "OrderId"    UUID     NOT NULL,
    "ProductId"  UUID     NOT NULL,
    "Quantity"   SMALLINT NOT NULL,
    "TotalPrice" DECIMAL  NOT NULL,
    FOREIGN KEY ("OrderId") REFERENCES "orders" ("Id"),
    FOREIGN KEY ("ProductId") REFERENCES "products" ("Id")
);