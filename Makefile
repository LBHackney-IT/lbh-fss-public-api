.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build fss-public-api

.PHONY: serve
serve:
	docker-compose build fss-public-api && docker-compose up fss-public-api

.PHONY: shell
shell:
	docker-compose run fss-public-api bash

.PHONY: test
test:
	docker-compose up test-database & docker-compose build fss-public-api-test && docker-compose up fss-public-api-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format
