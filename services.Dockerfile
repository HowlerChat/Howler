# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS howlerbuild
WORKDIR /source

# copy and publish app and libraries
COPY ./Howler.Database Howler.Database
COPY ./Howler.Services Howler.Services

# prevent binary and object files from getting copied downstream
RUN rm -rf ./Howler.Database/bin/
RUN rm -rf ./Howler.Database/obj/
RUN rm -rf ./Howler.Services/bin/
RUN rm -rf ./Howler.Services/obj/

# restore and publish
WORKDIR /source/Howler.Services
RUN dotnet restore -r linux-x64
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore -p:PublishReadyToRun=true

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-amd64
WORKDIR /app
COPY --from=howlerbuild /app .

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
#RUN apk add --no-cache icu-libs
#ENV LC_ALL=en_US.UTF-8
#ENV LANG=en_US.UTF-8
EXPOSE 5000

ENTRYPOINT ["./Howler.Services"]
