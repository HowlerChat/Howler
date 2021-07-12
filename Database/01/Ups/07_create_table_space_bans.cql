CREATE TABLE "howler"."space_bans"(
	"space_id" ASCII,
	"member_id" ASCII,
    "effective_date" TIMESTAMP,
    "end_date" TIMESTAMP,
    "reason_text" TEXT,
    "reason_message_id" ASCII,
    "reason_message_content" BLOB,
    "banning_member_id" ASCII,
	PRIMARY KEY("space_id", "member_id", "effective_date"))