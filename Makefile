DNAME:=$(shell date +'%Y%m%d-%H%M%S')

backend-projects:= Assert Config Reflection.Analyze AssertTest ConsoleTestBed Reflection.Analyzer ExecuterLib Reflection.Editable.ClassGenerator ExecuteTest Reflection.Editable.ClassGeneratorTest Logging SchemaGenApp Common Logic SchemaGeneration xml.transformer Reflection SchemaGenTest

ui-projects:=Common.UI automation.ui UseCase.Designer UseCase.DesignerTest UseCase automation.uiTest automation.uiTestRunner

image:
	docker build -t ubta .

ver:
	docker run -it --rm ubta dotnet --version
backend: $(backend-projects)

ui: $(ui-projects)

build:
	echo ${DNAME}; \
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.sln

$(backend-projects):
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.$@/ubta.$@.csproj

$(ui-projects):
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.$@/ubta.$@.csproj

clean:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet clean

restore:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet restore ubta.sln

:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.Logging/ubta.Logging.csproj

Logging.Tests:
	docker run -it --rm -v $(PWD):/${DNAME} --workdir /${DNAME} ubta dotnet msbuild ubta.Logging.Tests/ubta.Logging.Tests.csproj
