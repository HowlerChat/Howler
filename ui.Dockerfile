FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-amd64 AS howleruibase
WORKDIR /app

FROM node:lts-buster-slim AS node_base
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim-amd64 AS howleruibuild
COPY --from=node_base . .

WORKDIR /source

# copy and publish app and libraries
COPY ./Howler.App Howler.App

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.App/bin/
RUN rm -rf ./Howler.App/obj/

# restore and publish
WORKDIR /source/Howler.App/ClientApp

ENV NODE_ENV=production
RUN npm install
RUN npm install typescript
RUN npm run build

WORKDIR /source/Howler.App

RUN dotnet restore -r linux-musl-x64
RUN dotnet build -c release
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
FROM howleruibase as howlerui
WORKDIR /app
COPY --from=howleruibuild /app .

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
#RUN apk add --no-cache icu-libs
#ENV LC_ALL=en_US.UTF-8
#ENV LANG=en_US.UTF-8

ENTRYPOINT ["./Howler.App"]
