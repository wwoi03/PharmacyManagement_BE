#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PharmacyManagement_BE.API/PharmacyManagement_BE.API.csproj", "PharmacyManagement_BE.API/"]
COPY ["PharmacyManagement_BE.Application/PharmacyManagement_BE.Application.csproj", "PharmacyManagement_BE.Application/"]
COPY ["PharmacyManagement_BE.Infrastructure/PharmacyManagement_BE.Infrastructure.csproj", "PharmacyManagement_BE.Infrastructure/"]
COPY ["PharmacyManagement_BE.Domain/PharmacyManagement_BE.Domain.csproj", "PharmacyManagement_BE.Domain/"]
RUN dotnet restore "PharmacyManagement_BE.API/PharmacyManagement_BE.API.csproj"
COPY . .
WORKDIR "/src/PharmacyManagement_BE.API"
RUN dotnet build "PharmacyManagement_BE.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmacyManagement_BE.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmacyManagement_BE.API.dll"]