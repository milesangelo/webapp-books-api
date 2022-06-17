FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base-stage
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-stage
WORKDIR /src
COPY ["Books.Api/Books.Api.csproj", "Books.Api/"]
RUN dotnet restore "Books.Api/Books.Api.csproj"
COPY . .
WORKDIR "/src/Books.Api"
RUN dotnet build "Books.Api.csproj" -c Release -o /app/build

FROM build-stage AS publish-stage
RUN dotnet publish "Books.Api.csproj" -c Release -o /app/publish

FROM base-stage AS final-stage
WORKDIR /app
COPY --from=publish-stage /app/publish .
ENTRYPOINT ["dotnet", "Books.Api.dll"]

FROM nginx:alpine AS prod-stage
RUN mkdir -p /app
COPY --from=final-stage /app /app
COPY nginx.conf /etc/nginx/nginx.conf
