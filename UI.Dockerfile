FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . .
#RUN ls

# copy and publish app and libraries
RUN dotnet restore "TestSuite.WebUI/TestSuite.WebUI.csproj"
RUN dotnet publish "TestSuite.WebUI/TestSuite.WebUI.csproj" -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TestSuite.WebUI.dll"]
