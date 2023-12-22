DNAME:=$(shell date +'%Y%m%d-%H%M%S')
DOCKER_ARGS:=-v $(PWD):/${DNAME} -v ./home:/home/nonroot --workdir /${DNAME}

# alias dnet='docker run -it --rm -v .:/app --workdir /app ubta dotnet '

backend-projects:= Logging Common SampleLib Assert AssertTest Logic Config ConsoleTestBed xml.transformer Reflection Reflection.Analyze Reflection.Analyzer ExecuterLib SchemaGeneration SchemaGenTest

ui-projects:=Common.UI Reflection.Editable.ClassGenerator Reflection.Editable.ClassGeneratorTest  automation.ui  UseCase UseCase.Designer UseCase.DesignerTestExecuteTest SchemaGenApp automation.uiTest automation.uiTestRunner

define add_suffix_create_targets
$(eval projects:=$(1))
$(eval suffix:=$(2))
$(eval targets:=$(addsuffix -$(suffix), $(projects)))
$(targets):\
	$(eval project:=$(subst -$(suffix),,$(target)))\
    docker run -it --rm $(DOCKER_ARGS)  ubta dotnet $(suffix) ubta.$(project)/ubta.$(project).csproj

endef


image:
	# uid=1000(abnsharm) gid=1000(abnsharm)
	# provide uid and gid as arguments to docker build
	docker build --build-arg UID=1000 --build-arg GID=1000 -t ubta .

bash:
	docker run -it --rm $(DOCKER_ARGS)  ubta bash

ver:
	docker run -it --rm ubta dotnet --version

backend: $(backend-projects)



ui: $(ui-projects)

build:
	echo ${DNAME}; \
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet msbuild ubta.sln

$(backend-projects):
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet msbuild ubta.$@/ubta.$@.csproj

# add "-clean" to all entries in backend-projects variable
clean-backend-projects:= $(addsuffix -clean, $(backend-projects))
clean-backend: $(clean-backend-projects)
$(clean-backend-projects):
	# remove suffix "-clean" from all entries in backend-projects variable
	$(eval target:=$(subst -clean,,$@))
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet clean ubta.$(target)/ubta.$(target).csproj

# call add_suffix_create_targets function to create targets for restore
#$(call add_suffix_create_targets, $(backend-projects), restore)

restore-backend-projects:= $(addsuffix -restore, $(backend-projects))
restore-backend: $(restore-backend-projects)
$(restore-backend-projects):
	# remove suffix "-restore" from all entries in backend-projects variable
	$(eval target:=$(subst -restore,,$@))
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet restore ubta.$(target)/ubta.$(target).csproj

msbuild-backend-projects:= $(addsuffix -msbuild, $(backend-projects))
msbuild-backend: $(msbuild-backend-projects)
$(msbuild-backend-projects):
	# remove suffix "-msbuild" from all entries in backend-projects variable
	$(eval target:=$(subst -msbuild,,$@))
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet msbuild ubta.$(target)/ubta.$(target).csproj

run-backend-projects:= $(addsuffix -run, $(backend-projects))
run-backend: $(msbuild-backend-projects)
$(run-backend-projects):
	# remove suffix "-run" from all entries in backend-projects variable
	$(eval target:=$(subst -run,,$@))
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet run --project ubta.$(target)/ubta.$(target).csproj


$(ui-projects):
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet msbuild ubta.$@/ubta.$@.csproj

clean: 
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet clean

restore:
	docker run -it --rm $(DOCKER_ARGS)  ubta dotnet restore ubta.sln

