CREATE TABLE "howler"."channels"(
	"space_id" ASCII,
	"channel_id" ASCII,
	"channel_name" text,
	"created_date" timestamp,
	PRIMARY KEY(("space_id", "channel_id")))