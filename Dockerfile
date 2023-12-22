FROM  mcr.microsoft.com/dotnet/sdk:7.0
ARG UID
ARG GID
RUN apt update -y
RUN apt install sudo 

RUN addgroup --gid $GID nonroot && \
    adduser --uid $UID --gid $GID --disabled-password --gecos "" nonroot && \
    echo 'nonroot ALL=(ALL) NOPASSWD: ALL' >> /etc/sudoers

ENV DOTNET_CLI_TELEMETRY_OPTOUT true
RUN dotnet workload update

# Set the non-root user as the default user
USER nonroot
