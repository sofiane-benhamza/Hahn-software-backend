# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:7.0 
WORKDIR /app

# Copy solution and all project folders
COPY TodoListApp.sln ./
COPY TodoListApp.Domain/ TodoListApp.Domain/
COPY TodoListApp.Application/ TodoListApp.Application/
COPY TodoListApp.Infrastructure/ TodoListApp.Infrastructure/
COPY TodoListApp.API/ TodoListApp.API/
COPY TodoListApp.Tests/ TodoListApp.Tests/

# Restore dependencies using solution
RUN dotnet restore TodoListApp.sln

# Copy everything else and build
COPY . .
RUN dotnet publish TodoListApp.API/TodoListApp.API.csproj -c Release -o out

# Install dotnet-ef as a local tool
RUN dotnet new tool-manifest || echo "tool manifest exists"
RUN dotnet tool install dotnet-ef --version 7.*



# Set environment variables (optional)
# ENV DEFAULT_CONNECTION="Server=db;Database=hahn_todolist;User Id=SA;Password=Hahn2025!;Encrypt=False;"

# Run migrations then start API
ENTRYPOINT ["sh", "-c", "dotnet tool restore && \
dotnet tool run dotnet-ef database update -p TodoListApp.Infrastructure/TodoListApp.Infrastructure.csproj -s TodoListApp.API/TodoListApp.API.csproj && \
dotnet out/TodoListApp.API.dll"]
