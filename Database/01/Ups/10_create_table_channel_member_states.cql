CREATE TABLE "howler"."channel_member_states"(
    "space_id" ASCII,
	"channel_id" ASCII,
	"member_id" ASCII,
	"last_read_message_id" ASCII,
    "mention_count" INT,
    "mention_type" ASCII,
	PRIMARY KEY(("space_id", "member_id"), "channel_id"))