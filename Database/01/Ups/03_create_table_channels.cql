CREATE TABLE "howler"."channels"(
	"space_id" ASCII,
	"channel_id" ASCII,
	"channel_name" text,
	"created_date" timestamp,
	"modified_date" timestamp,
	"member_id" ASCII,
	"encryption_method" ASCII,
	"channel_policy" blob,
	"channel_topic" text,
	PRIMARY KEY(("space_id", "channel_id")))