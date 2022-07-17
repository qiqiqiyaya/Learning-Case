
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# 设置工作目录
WORKDIR /app

# 把目录下的内容都复制到当前目录下
COPY . .

# 暴露80端口
EXPOSE 80

# 运行镜像入口命令和可执行文件名称
ENTRYPOINT ["dotnet", "Redis.Distributed.dll"]