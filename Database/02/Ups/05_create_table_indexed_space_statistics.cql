CREATE TABLE "howler_indexer"."indexed_space_statistics"(
	"server_id" ASCII,
	"space_id" ASCII,
	"user_count" counter
	PRIMARY KEY("server_id", "space_id"))