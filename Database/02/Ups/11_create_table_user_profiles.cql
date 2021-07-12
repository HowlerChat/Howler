CREATE TABLE "howler_indexer"."user_profiles"(
	"user_id" ASCII,
	"display_name" TEXT,
	"status" TEXT,
	"status_emoji" ASCII,
	"icon_url" ASCII,
	"banner_url" ASCII,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
	"profile_data" BLOB,
	PRIMARY KEY("user_id"))