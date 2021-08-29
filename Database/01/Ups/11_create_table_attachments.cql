CREATE TABLE "howler"."attachments"(
	"attachment_id" ASCII,
    "member_id" ASCII,
	"upload_date" TIMESTAMP,
    "file_name" TEXT,
    "file_type" ASCII,
    "file_size" INT,
	"metadata" blob,
	PRIMARY KEY("attachment_id"))