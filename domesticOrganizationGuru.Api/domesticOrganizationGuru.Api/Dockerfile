#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 44365

ENV ASPNETCORE_URLS=https://+:44365

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["domesticOrganizationGuru.Api/DomesticOrganizationGuru.Api.csproj", "domesticOrganizationGuru.Api/"]
COPY ["domesticOrganizationGuru.SignalR/domesticOrganizationGuru.SignalR.csproj", "domesticOrganizationGuru.SignalR/"]
COPY ["domesticOrganizationGuru.Common/domesticOrganizationGuru.Common.csproj", "domesticOrganizationGuru.Common/"]
COPY ["domesticOrganizationGuru.Repository/domesticOrganizationGuru.Repository.csproj", "domesticOrganizationGuru.Repository/"]
COPY ["domesticOrganizationGuru.Entities/domesticOrganizationGuru.Entities.csproj", "domesticOrganizationGuru.Entities/"]
COPY ["domesticOrganizationGuru.Logger/domesticOrganizationGuru.Logger.csproj", "domesticOrganizationGuru.Logger/"]
COPY ["domesticOrganizationGuru.Validation/domesticOrganizationGuru.Validation.csproj", "domesticOrganizationGuru.Validation/"]
COPY ["domesticOrganizationGuru.AutoMapper/domesticOrganizationGuru.AutoMapper.csproj", "domesticOrganizationGuru.AutoMapper/"]
COPY ["domesticOrganizationGuru.Redis/domesticOrganizationGuru.Redis.csproj", "domesticOrganizationGuru.Redis/"]
RUN dotnet restore "domesticOrganizationGuru.Api/DomesticOrganizationGuru.Api.csproj"
COPY . .
WORKDIR "/src/domesticOrganizationGuru.Api"
RUN dotnet build "DomesticOrganizationGuru.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomesticOrganizationGuru.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomesticOrganizationGuru.Api.dll"]