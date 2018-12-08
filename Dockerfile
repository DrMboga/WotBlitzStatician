# Angular part
FROM node as angular-build
WORKDIR /app

COPY WotBlitzStatician/package*.json /app/

RUN npm install

COPY WotBlitzStatician/angular.json /app/
COPY WotBlitzStatician/tsconfig.json /app/
COPY WotBlitzStatician/tslint.json /app/
COPY WotBlitzStatician/client-src/. /app/client-src/

RUN npm run build -- --output-path=./dist/out

# ToDo: Run tests

# dotnetcore part
FROM microsoft/dotnet:2.1-sdk AS netcore-build
WORKDIR /app

COPY WotBlitzStatician.sln .
COPY WotBlitzStatician.Model/*.csproj ./WotBlitzStatician.Model/
COPY WotBlitzStatician.WotApiClient/*.csproj ./WotBlitzStatician.WotApiClient/
COPY WotBlitzStatician.Data/*.csproj ./WotBlitzStatician.Data/
COPY WotBlitzStatician.Logic/*.csproj ./WotBlitzStatician.Logic/
COPY WotBlitzStatician/*.csproj ./WotBlitzStatician/
COPY WotBlitzStatician.Logic.Test/*.csproj ./WotBlitzStatician.Logic.Test/
RUN dotnet restore

COPY WotBlitzStatician.Model/. ./WotBlitzStatician.Model/
COPY WotBlitzStatician.WotApiClient/. ./WotBlitzStatician.WotApiClient/
COPY WotBlitzStatician.Data/. ./WotBlitzStatician.Data/
COPY WotBlitzStatician.Logic/. ./WotBlitzStatician.Logic/
COPY WotBlitzStatician.Logic.Test/*.csproj ./WotBlitzStatician.Logic.Test/
COPY WotBlitzStatician/. ./WotBlitzStatician/

# Unit tests (not working - not found any xunit test)
WORKDIR /app/WotBlitzStatician.Logic.Test
RUN dotnet test

# Finally publish in prod mode
WORKDIR /app/WotBlitzStatician
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=netcore-build /app/WotBlitzStatician/out ./
COPY --from=angular-build /app/dist/out ./wwwroot/
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "WotBlitzStatician.dll"]
