-- PostgreSQL schema and master configuration

CREATE SCHEMA IF NOT EXISTS schema1;
CREATE SCHEMA IF NOT EXISTS schema2;

-- Example log tables based on master config
CREATE TABLE IF NOT EXISTS schema1."Logs" (
    "Id" serial PRIMARY KEY,
    "ClientId" text,
    "RawJson" jsonb,
    "Timestamp" timestamptz,
    "eventType" text,
    "status" text
);

CREATE TABLE IF NOT EXISTS schema2."Logs" (
    "Id" serial PRIMARY KEY,
    "ClientId" text,
    "RawJson" jsonb,
    "Timestamp" timestamptz,
    "level" text,
    "message" text
);