#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ESS.Admin.WebHost/ESS.Admin.WebHost.csproj", "ESS.Admin.WebHost/"]
COPY ["ESS.Admin.DataAccess/ESS.Admin.DataAccess.csproj", "ESS.Admin.DataAccess/"]
COPY ["ESS.Admin.Core/ESS.Admin.Core.csproj", "ESS.Admin.Core/"]
RUN dotnet restore "ESS.Admin.WebHost/ESS.Admin.WebHost.csproj"
COPY . .
WORKDIR "/src/ESS.Admin.WebHost"
RUN dotnet build "ESS.Admin.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESS.Admin.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESS.Admin.WebHost.dll"]
