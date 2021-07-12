CREATE TABLE "howler_config"."user_authorizers"(
	"user_id" ASCII,
	"authorizers" MAP<TEXT, TEXT>,
	PRIMARY KEY("user_id"))