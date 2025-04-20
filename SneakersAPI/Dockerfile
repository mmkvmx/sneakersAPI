# 1. Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Копируем проект в контейнер
COPY . .

# Восстанавливаем зависимости и публикуем
RUN dotnet restore
RUN dotnet publish -c Release -o out

# 2. Финальный контейнер
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Копируем собранный проект
COPY --from=build /app/out .

# Открываем порт 3000 (тот, что слушает твой .NET API)
EXPOSE 3000

# Запускаем приложение
ENTRYPOINT ["dotnet", "SneakersAPI.dll"]
