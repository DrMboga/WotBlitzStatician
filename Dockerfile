FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WotBlitzStatician.Model/*.csproj ./WotBlitzStatician.Model/
COPY WotBlitzStatician.WotApiClient/*.csproj ./WotBlitzStatician.WotApiClient/
COPY WotBlitzStatician.Data/*.csproj ./WotBlitzStatician.Data/
COPY WotBlitzStatician.Logic/*.csproj ./WotBlitzStatician.Logic/
COPY WotBlitzStatician/*.csproj ./WotBlitzStatician/
RUN dotnet restore

# copy everything else and build app
COPY WotBlitzStatician.Model/. ./WotBlitzStatician.Model/
COPY WotBlitzStatician.WotApiClient/. ./WotBlitzStatician.WotApiClient/
COPY WotBlitzStatician.Data/. ./WotBlitzStatician.Data/
COPY WotBlitzStatician.Logic/. ./WotBlitzStatician.Logic/
COPY WotBlitzStatician/. ./WotBlitzStatician/
WORKDIR /app/WotBlitzStatician
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY entrypoint.sh ./entrypoint.sh
COPY --from=build /app/WotBlitzStatician/out ./
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "WotBlitzStatician.dll"]
# RUN chmod +x ./entrypoint.sh
# CMD /bin/bash ./entrypoint.sh