# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS authgatewaybuild
WORKDIR /source

COPY ./Howler.Database Howler.Database
COPY ./Howler.AuthGateway Howler.AuthGateway

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.Database/bin/
RUN rm -rf ./Howler.Database/obj/
RUN rm -rf ./Howler.AuthGateway/bin/
RUN rm -rf ./Howler.AuthGateway/obj/

# restore and publish
WORKDIR /source/Howler.AuthGateway
RUN dotnet restore -r linux-musl-x64
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine-amd64
WORKDIR /app
COPY --from=authgatewaybuild /app .

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
#RUN apk add --no-cache icu-libs
#ENV LC_ALL=en_US.UTF-8
#ENV LANG=en_US.UTF-8


ENTRYPOINT ["./Howler.AuthGateway"]