Database Setup:
This app using Microsoft SQL Server and will require SSMS or an alternative.

1. Navigate to the root directory of the WebApi cd /WebAPI
2. dotnet ef database update

Start without Docker

After setting up the database:

1. from the root directory cd /WebAPI
2. dotnet restore
3. start the backend with dotnet run

After the backend is started let's start the frontend:

1. in a new terminal navigate to the WebApp cd /WebApp or cd ../WebApp depending on your current directory.
2. npm install
3. ng serve
4. Navigate to localhost:4200

Start with Docker

1. Ensure Docker Desktop is started
2. Navigate to the root directory and use the command: docker-compose up
3. Navigate to localhost:4200
