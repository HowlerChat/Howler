CREATE TABLE "howler"."blacklist"(
	"howler_id" ASCII,
	"entity_type" TEXT,
	"reason" TEXT,
	"blacklisting_user_id" ASCII,
    "blacklisted_date" TIMESTAMP,
    "blacklisted_until_date" TIMESTAMP,
	PRIMARY KEY("howler_id", "entity_type"))
WITH CLUSTERING ORDER BY("entity_type" ASC)