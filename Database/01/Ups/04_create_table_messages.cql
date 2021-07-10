CREATE TABLE "howler"."messages"(
	"channel_id" ASCII,
    "message_id" ASCII,
	"created_date" TIMESTAMP,
	"modified_date" TIMESTAMP,
	"content" blob,
	PRIMARY KEY("channel_id", "created_date"))
WITH CLUSTERING ORDER BY("created_date" ASC)