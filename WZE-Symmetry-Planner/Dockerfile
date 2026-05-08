# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and all project files first (for layer caching)
COPY ["WZE-Symmetry-Planner.sln", "."]
COPY ["WZE-Symmetry-Planner/WZE-Symmetry-Planner.csproj", "WZE-Symmetry-Planner/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "WZE-Symmetry-Planner/WZE-Symmetry-Planner.csproj"

# Copy all source
COPY . .

RUN dotnet publish "WZE-Symmetry-Planner/WZE-Symmetry-Planner.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Install EF tools in build stage so migrations can run at startup
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Copy EF tools
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy SDK for running EF migrations at startup (needed by RunCommand in Program.cs)
COPY --from=build /usr/share/dotnet /usr/share/dotnet
ENV DOTNET_ROOT=/usr/share/dotnet

# Copy source for migrations (ef needs the project files)
COPY --from=build /src/Infrastructure /src/Infrastructure
COPY --from=build /src/WZE-Symmetry-Planner /src/WZE-Symmetry-Planner
COPY --from=build /src/Domain /src/Domain
COPY --from=build /src/Application /src/Application

EXPOSE 8080

ENTRYPOINT ["dotnet", "WZE-Symmetry-Planner.dll"]
