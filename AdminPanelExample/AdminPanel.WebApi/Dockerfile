FROM mcr.microsoft.com/dotnet/sdk:7.0 AS prepare-restore-files
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset --version 0.3.2
WORKDIR /source
COPY . .
RUN dotnet subset restore AdminPanel.WebApi/AdminPanel.WebApi.csproj --root-directory /source --output restore_subset/

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY --from=prepare-restore-files /source/restore_subset .
RUN dotnet restore AdminPanel.WebApi/AdminPanel.WebApi.csproj

COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AdminPanel.WebApi.dll"]
