CREATE KEYSPACE "howler_indexer"
WITH replication = {'class':'SimpleStrategy', 'replication_factor' : 3}