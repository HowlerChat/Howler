CREATE TABLE "howler_indexer"."indexed_spaces"(
	"server_id" ASCII,
	"space_id" ASCII,
	"space_name" TEXT,
	"description" TEXT,
	"vanity_url" TEXT,
	"icon_url" ASCII,
	"banner_url" ASCII,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
	"user_id" ASCII,
	"default_channel_id" ASCII,
	PRIMARY KEY("server_id", "space_id"))