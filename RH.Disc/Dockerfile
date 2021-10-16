#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RH.Disc/RH.Disc.csproj", "RH.Disc/"]
RUN dotnet restore "RH.Disc/RH.Disc.csproj"
COPY . .
WORKDIR "/src/RH.Disc"
RUN dotnet build "RH.Disc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RH.Disc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RH.Disc.dll"]