CREATE TABLE "howler"."space_history"(
	"space_id" ASCII,
    "change_date" TIMESTAMP,
    "user_id" ASCII,
    "change" BLOB,
	PRIMARY KEY("space_id", "change_date"))