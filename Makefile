DNAME:=$(shell date +'%Y%m%d-%H%M%S')

image:
	docker build -t ubta .

ver:
	docker run -it --rm ubta dotnet --version

build:
	echo ${DNAME}; \
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.sln

clean:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet clean

restore:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet restore ubta.sln

Logging:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.Logging/ubta.Logging.csproj

Logging.Tests:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.Logging.Tests/ubta.Logging.Tests.csproj
