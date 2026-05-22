.PHONY: all build test run clean restore docker-build docker-up docker-down help

PROJECT_NAME := LeaseBuyArch
SLN_DIR := Src
SLN := $(SLN_DIR)/$(PROJECT_NAME).sln

all: restore build

help:
	@echo "Usage:"
	@echo "  make restore     - Restore NuGet packages"
	@echo "  make build       - Build the solution"
	@echo "  make test        - Run all tests"
	@echo "  make run         - Run the application"
	@echo "  make clean       - Clean build artifacts"
	@echo "  make docker-build - Build Docker image"
	@echo "  make docker-up   - Start Docker Compose (app + postgres)"
	@echo "  make docker-down - Stop Docker Compose"

restore:
	dotnet restore $(SLN)

build: restore
	dotnet build $(SLN) --no-restore

test:
	dotnet test $(SLN) --no-restore

run: build
	dotnet run --project $(SLN_DIR)/$(PROJECT_NAME)/$(PROJECT_NAME).csproj

clean:
	dotnet clean $(SLN)
	rm -rf $(SLN_DIR)/**/bin/ $(SLN_DIR)/**/obj/

docker-build:
	docker compose -f $(SLN_DIR)/docker-compose.yml build

docker-up:
	docker compose -f $(SLN_DIR)/docker-compose.yml up -d

docker-down:
	docker compose -f $(SLN_DIR)/docker-compose.yml down
