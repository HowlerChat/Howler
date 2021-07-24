FROM public.ecr.aws/lambda/dotnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS indexerbuild
WORKDIR /source

COPY ./Howler.Services Howler.Services
COPY ./Howler.Database Howler.Database
COPY ./Howler.Database.Indexer Howler.Database.Indexer
COPY ./Howler.Indexer Howler.Indexer

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.Services/bin/
RUN rm -rf ./Howler.Services/obj/
RUN rm -rf ./Howler.Database/bin/
RUN rm -rf ./Howler.Database/obj/
RUN rm -rf ./Howler.Database.Indexer/bin/
RUN rm -rf ./Howler.Database.Indexer/obj/
RUN rm -rf ./Howler.Indexer/bin/
RUN rm -rf ./Howler.Indexer/obj/

# restore and publish
WORKDIR /source/Howler.Indexer
RUN dotnet restore -r linux-x64
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false -p:PublishReadyToRun=true

# final stage/image
FROM base as final
WORKDIR /var/task
COPY --from=indexerbuild /app .