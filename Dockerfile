FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_URLS=http://*:${PORT}
COPY bin/Debug/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "CloudRun-DotNetCore.dll"]