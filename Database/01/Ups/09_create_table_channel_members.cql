CREATE TABLE "howler"."channel_members"(
	"channel_id" ASCII,
	"member_id" ASCII,
	"profile_data" BLOB,
	PRIMARY KEY("channel_id", "member_id"))