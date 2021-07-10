CREATE TABLE "howler"."spaces"(
	"space_id" ASCII,
	"space_name" TEXT,
	"description" TEXT,
	"vanity_url" TEXT,
	"server_url" TEXT,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
	"user_id" ASCII,
	"default_channel_id" ASCII,
	"space_policy" blob,
	"channel_groups" blob,
	PRIMARY KEY("space_id", "server_url"))
WITH CLUSTERING ORDER BY("server_url" ASC)