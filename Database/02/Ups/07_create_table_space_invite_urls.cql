CREATE TABLE "howler_indexer"."space_invite_urls"(
	"invite_url" ASCII,
	"space_id" ASCII,
    "landing_channel_id" ASCII,
    "user_id" ASCII,
    "expire_type" ASCII,
    "expires_after" TIMESTAMP,
	PRIMARY KEY("invite_url"))