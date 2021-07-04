FROM public.ecr.aws/lambda/dotnet:5.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS authgatewaybuild
WORKDIR /source

COPY ./Howler.Services Howler.Services
COPY ./Howler.Database Howler.Database
COPY ./Howler.AuthGateway Howler.AuthGateway

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.Services/bin/
RUN rm -rf ./Howler.Services/obj/
RUN rm -rf ./Howler.Database/bin/
RUN rm -rf ./Howler.Database/obj/
RUN rm -rf ./Howler.AuthGateway/bin/
RUN rm -rf ./Howler.AuthGateway/obj/

# restore and publish
WORKDIR /source/Howler.AuthGateway
RUN dotnet restore -r linux-x64
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false -p:PublishReadyToRun=true

# final stage/image
FROM base as final
WORKDIR /var/task
COPY --from=authgatewaybuild /app .