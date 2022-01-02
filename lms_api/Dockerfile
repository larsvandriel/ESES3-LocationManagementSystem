#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LocationManagementSystem/lms_api/lms_api.csproj", "LocationManagementSystem/lms_api/"]
RUN dotnet restore "LocationManagementSystem/lms_api/lms_api.csproj"
COPY . .
WORKDIR "/src/LocationManagementSystem/lms_api"
RUN dotnet build "lms_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lms_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lms_api.dll"]