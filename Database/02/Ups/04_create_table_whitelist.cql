CREATE TABLE "howler"."whitelist"(
	"howler_id" ASCII,
	"entity_type" TEXT,
	"reason" TEXT,
	"whitelisting_user_id" ASCII,
    "whitelisted_date" TIMESTAMP,
    "whitelisted_until_date" TIMESTAMP,
	PRIMARY KEY("howler_id", "entity_type"))
WITH CLUSTERING ORDER BY("entity_type" ASC)