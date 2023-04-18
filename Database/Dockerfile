FROM mcr.microsoft.com/azure/sql-edge:latest
ENV ACCEPT_EULA=Y
ENV MSSQL_USER=SA
ENV SA_PASSWORD=97Y59K46Mawu6o4
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433
ENV NAME = NourishNexusDatabase
WORKDIR /src
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started"