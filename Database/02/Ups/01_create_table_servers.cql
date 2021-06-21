CREATE TABLE "howler"."servers"(
	"server_id" ASCII,
	"server_name" TEXT,
	"description" TEXT,
	"server_url" TEXT,
    "entitlements" TEXT,
    "user_id" ASCII,
    "country_id" ASCII,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
    "blacklisted_date" TIMESTAMP,
	PRIMARY KEY("server_id", "server_url"))
WITH CLUSTERING ORDER BY("server_url" ASC)