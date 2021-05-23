#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Development
export HOWLER_CASSANDRA_ENDPOINT=127.0.0.1
export HOWLER_KEYSPACE=howler
export HOWLER_ENVIRONMENT=dev
export HOWLER_SCOPE=openid

dotnet run
