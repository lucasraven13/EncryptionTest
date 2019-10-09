#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 54412

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY EncryptionService/EncryptionService.csproj EncryptionService/
RUN dotnet restore EncryptionService/EncryptionService.csproj
COPY . .
WORKDIR /src/EncryptionService
RUN dotnet build EncryptionService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EncryptionService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EncryptionService.dll"]
