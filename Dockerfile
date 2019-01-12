FROM microsoft/dotnet:2.2-sdk
ARG configuration=Debug
WORKDIR /project/
COPY . .
RUN dotnet build --configuration ${configuration} Peyk.Matrix.Client.sln
