CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "orders"
(
    "Id"    UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "IdCostumer" UUID NOT NULL,
    "Status"  INT NOT NULL,
    "CreateDate" DATETIME NOT NULL,
    "PriceTotal" DECIMAL,
    CONSTRAINT "fk_orders_costumer" FOREIGN KEY ("IdOrder") REFERENCES orders ("Id") 
);

CREATE TABLE "ordersItem"
(
    "Id"    UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "IdOrder" UUID NOT NULL,
    "IdProduct" UUID NOT NULL,
    "Quantity" INT NOT NULL,
    "PriceTotal" DECIMAL NOT NULL,
    CONSTRAINT "fk_ordersitem_order" FOREIGN KEY ("IdOrder") REFERENCES orders ("Id"),
    CONSTRAINT "fk_ordersitem_product" FOREIGN KEY ("IdProduct") REFERENCES products ("Id")      

);