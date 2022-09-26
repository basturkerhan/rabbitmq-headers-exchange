FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish UdemyRabbitMQ.subscriber.csproj -c Release -o publish

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=build /app/publish .

ENV URI=""
ENTRYPOINT ["dotnet", "UdemyRabbitMQ.subscriber.dll"]