# Entrega
 RESTful API project

 to run the database on docker:

    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=PassCode#2024" -p 1450:1433 --name sqlserverdb --hostname wizardstoredb -d mcr.microsoft.com/mssql/server:2019-latest

 the following docker images are the ones supposed to be used in this project:

    docker pull mcr.microsoft.com/mssql/server:2019-latest
    docker pull mcr.microsoft.com/dotnet/sdk:6.0

 if any alterations are made to the entity model, run the following command befor running the code:

    dotnet ef migrations add [migrationName]

 to do list:

    authentication is a bit buggy, something to do with the process of comparing the input to what is on the database, so it fails in situations where it should succeed.

    formating issues in the posting request

    unit testing was not done yet, should be done eventually

    once everything is working, the dockerfile needs to be made so that everything is containerized. the database already runs in docker, but the api doesnt yet.
