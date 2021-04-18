CREATE TABLE "howler"."spaces"(
	"space_id" ASCII,
	"space_name" TEXT,
	"description" TEXT,
	"vanity_url" TEXT,
	"server_url" TEXT,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
	PRIMARY KEY("space_id", "server_url"))
WITH CLUSTERING ORDER BY("server_url" ASC)