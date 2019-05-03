FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["ToDo.Web/ToDo.Web.csproj", "ToDo.Web/"]
RUN dotnet restore "ToDo.Web/ToDo.Web.csproj"
COPY . .
WORKDIR "/src/ToDo.Web"
RUN dotnet build "ToDo.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ToDo.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ToDo.Web.dll"]