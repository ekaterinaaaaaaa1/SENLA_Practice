CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE inactivepassports (
    passp_series smallint NOT NULL,
    passp_number integer NOT NULL,
    active boolean NOT NULL,
    CONSTRAINT "PK_inactivepassports" PRIMARY KEY (passp_series, passp_number)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241111125336_InitialCreate', '8.0.10');

COMMIT;

