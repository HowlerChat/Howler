FROM public.ecr.aws/lambda/dotnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS configbuild
WORKDIR /source

COPY ./Howler.Services Howler.Services
COPY ./Howler.Database Howler.Database
COPY ./Howler.Database.Config Howler.Database.Config
COPY ./Howler.Config Howler.Config

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.Services/bin/
RUN rm -rf ./Howler.Services/obj/
RUN rm -rf ./Howler.Database/bin/
RUN rm -rf ./Howler.Database/obj/
RUN rm -rf ./Howler.Database.Config/bin/
RUN rm -rf ./Howler.Database.Config/obj/
RUN rm -rf ./Howler.Config/bin/
RUN rm -rf ./Howler.Config/obj/

# restore and publish
WORKDIR /source/Howler.Config
RUN dotnet restore -r linux-x64
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false -p:PublishReadyToRun=true

# final stage/image
FROM base as final
WORKDIR /var/task
COPY --from=configbuild /app .