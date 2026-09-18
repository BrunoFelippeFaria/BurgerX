# Backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend

WORKDIR /src
COPY backend/ .
RUN dotnet publish -c Release -o /app/publish

# Frontend
FROM node:22 AS frontend

WORKDIR /frontend
COPY frontend/ .
RUN npm ci
RUN npm run build

# Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=backend /app/publish .
COPY --from=frontend /frontend/dist ./wwwroot

EXPOSE 8080

ENTRYPOINT ["dotnet", "BurgerX.Api.dll"]