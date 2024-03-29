#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["DatingWebAppScratch.csproj", "DatingWebAppScratch/"]
RUN dotnet restore "DatingWebAppScratch/DatingWebAppScratch.csproj"
WORKDIR "/src/DatingWebAppScratch"
COPY . .

RUN dotnet build "DatingWebAppScratch.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DatingWebAppScratch.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DatingWebAppScratch.dll"]