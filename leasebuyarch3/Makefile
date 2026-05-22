.PHONY: all help restore build build-common build-leasing build-monolith \
        test run run-monolith run-leasing clean \
        docker-build docker-up docker-down docker-logs

DOTNET := dotnet
COMMON_DIR := LeaseBuyArch.Common
COMMON_SLN := $(COMMON_DIR)/LeaseBuyArch.Common.sln
LEASING_DIR := Leasing
LEASING_SLN := $(LEASING_DIR)/LeaseBuyArch.Leasing.sln
MONOLITH_DIR := ModularMonolith
MONOLITH_SLN := $(MONOLITH_DIR)/LeaseBuyArch.slnx

all: restore build

help:
	@echo "Usage:"
	@echo "  make              - Restore + build all solutions"
	@echo "  make restore      - Restore NuGet packages for all solutions"
	@echo "  make build        - Build all solutions"
	@echo "  make build-common - Build Common packages only"
	@echo "  make build-leasing - Build Leasing microservice only"
	@echo "  make build-monolith - Build Modular Monolith only"
	@echo "  make test         - Run all tests"
	@echo "  make run-monolith - Run Modular Monolith locally"
	@echo "  make run-leasing  - Run Leasing microservice locally"
	@echo "  make clean        - Clean all build artifacts"
	@echo "  make docker-build - Build Docker images for all services"
	@echo "  make docker-up    - Start all services via Docker Compose"
	@echo "  make docker-down  - Stop all Docker Compose services"
	@echo "  make docker-logs  - Tail logs from Docker Compose services"

restore:
	$(DOTNET) restore $(COMMON_SLN)
	$(DOTNET) restore $(LEASING_SLN)
	$(DOTNET) restore $(MONOLITH_SLN)

build: restore
	$(DOTNET) build $(COMMON_SLN) --no-restore
	$(DOTNET) build $(LEASING_SLN) --no-restore
	$(DOTNET) build $(MONOLITH_SLN) --no-restore

build-common:
	$(DOTNET) build $(COMMON_SLN)

build-leasing:
	$(DOTNET) build $(LEASING_SLN)

build-monolith:
	$(DOTNET) build $(MONOLITH_SLN)

test:
	$(DOTNET) test $(COMMON_SLN) --no-restore || true
	$(DOTNET) test $(LEASING_SLN) --no-restore || true
	$(DOTNET) test $(MONOLITH_SLN) --no-restore || true

run-monolith: build-monolith
	$(DOTNET) run --project $(MONOLITH_DIR)/LeaseBuyArch/LeaseBuyArch.csproj

run-leasing: build-leasing
	$(DOTNET) run --project $(LEASING_DIR)/LeaseBuyArch.Leasing/LeaseBuyArch.Leasing.csproj

clean:
	$(DOTNET) clean $(COMMON_SLN)
	$(DOTNET) clean $(LEASING_SLN)
	$(DOTNET) clean $(MONOLITH_SLN)
	find . -type d \( -name bin -o -name obj \) -exec rm -rf {} + 2>/dev/null || true

docker-build:
	docker compose build

docker-up:
	docker compose up -d

docker-down:
	docker compose down

docker-logs:
	docker compose logs -f
