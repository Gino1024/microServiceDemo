# ---------- STAGE 1: restore ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

# 只拷貝專案檔，最大化利用層級快取
COPY ms.infrastructure/*.csproj ms.infrastructure/
COPY ms.userapi/*.csproj ms.userapi/
COPY *.sln ./

RUN dotnet restore ms.userapi/ms.user.csproj

# 現在才拷貝完整原始碼
COPY . .

RUN dotnet publish ms.userapi/ms.user.csproj \
    -c Release \
    -o /out \
    /p:UseAppHost=false \
    --no-restore

# ---------- STAGE 3: runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
ARG GIT_SHA
ENV GIT_SHA=${GIT_SHA}
WORKDIR /app
# 可選：文件化容器 Port（K8s 不靠這個，但對本地 run 有幫助）
EXPOSE 8080

COPY --from=publish /out .

# 端口由環境變數控制（建議在 K8s 設定）
# ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "ms.user.dll"]
