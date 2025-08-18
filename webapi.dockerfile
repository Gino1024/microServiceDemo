# ---------- SDK: restore + publish 同一階段 ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 先拷 csproj（讓還原結果可被快取）
COPY ms.infrastructure/*.csproj ms.infrastructure/
COPY ms.webapi/*.csproj ms.webapi/
COPY *.sln ./

RUN dotnet restore ms.webapi/ms.webapi.csproj

# 再拷整個來源
COPY . .

# 因為跟 restore 同一個 stage，所以可以 --no-restore
RUN dotnet publish ms.webapi/ms.webapi.csproj \
    -c Release -o /out \
    /p:UseAppHost=false \
    --no-restore

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
EXPOSE 8080
COPY --from=build /out .
ENTRYPOINT ["dotnet", "ms.webapi.dll"]
