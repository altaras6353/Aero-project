Job Project
This repository contains three separate .NET applications and requires a local RabbitMQ server to function properly. Follow the steps below to build and run each part.

Prerequisites
.NET 6 (or later) installed on your machine.
RabbitMQ server running locally (default port 5672).
Projects Overview
REST API
Location: rest_api (example path)
Runs on: http://localhost:5190
Entities Generator (Point Generator)
Location: pointgenerator (example path)
Runs on: http://localhost:5290
Entities Presenter (Map Application / Razor Pages)
Location: entities_presenter (example path)
Runs on: http://localhost:5236
How to Run
Important: Make sure RabbitMQ is up and running before starting the .NET apps.

1. RabbitMQ
Install and run RabbitMQ locally.
By default, RabbitMQ listens on port 5672.
Once it's running, you can confirm by visiting http://localhost:15672 (if the management plugin is enabled).
2. Build and Run the REST API
Open a terminal/command prompt in the rest_api folder.
Build:
bash
Copy
Edit
dotnet build
Run:
bash
Copy
Edit
dotnet run
Once running, the API should be accessible at http://localhost:5190.
Note: You can see intermediate operations/logs in the terminal window.

3. Build and Run the Entities Generator
Open another terminal in the pointgenerator folder.
Build:
bash
Copy
Edit
dotnet build
Run:
bash
Copy
Edit
dotnet run
The generator will be accessible at http://localhost:5290.
Note: You can see intermediate logs and console output in this terminal as well.

4. Build and Run the Entities Presenter (Map Application)
Open a third terminal in the entities_presenter folder.
Build:
bash
Copy
Edit
dotnet build
Run:
bash
Copy
Edit
dotnet run
The map application should be accessible at http://localhost:5236.
Notes:

You will see logs in the terminal.
To view WebSocket connections and other network details, open your browser’s Developer Tools (press F12) and check the Network tab.
Troubleshooting
RabbitMQ Not Found

Make sure you have RabbitMQ installed and running locally on port 5672.
Check logs in the API or generator for any connection errors.
Port Already in Use or Changed

Change the port in the corresponding Properties/launchSettings.json file or other configuration within each project, and then rebuild/run.
Important: If you change any port, you must also update it in:
entities_createor > Pages > Index.cshtml (line 44)
entities_presentor > Pages > Index.cshtml (line 33)
