FROM microsoft/dotnet:2-sdk
WORKDIR /src
ADD . .
EXPOSE 80
RUN dotnet restore
RUN dotnet build
ENTRYPOINT dotnet run 0.0.0.0 80
