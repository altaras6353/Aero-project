# Job Project

This repository contains three separate .NET applications that communicate via a local RabbitMQ server. Follow the instructions below to build and run each part.

---

## Prerequisites

1. **.NET **  
   Download it here: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

2. **RabbitMQ**  
   Install RabbitMQ and ensure it is running locally on its default port (5672).  
   For installation instructions, see: [https://www.rabbitmq.com/download.html](https://www.rabbitmq.com/download.html)

---

## Project Overview

| Project               | Folder               | URL / Port                    |
|-----------------------|----------------------|--------------------------------|
| **REST API**          | `rest_api`          | [http://localhost:5190](http://localhost:5190) |
| **Entities Generator**| `pointgenerator`    | [http://localhost:5290](http://localhost:5290) |
| **Entities Presenter**| `entities_presenter`| [http://localhost:5236](http://localhost:5236) |

---

## How to Run

### Step 1: Set Up RabbitMQ

1. Install RabbitMQ and ensure it is running.
2. By default, RabbitMQ listens on port **5672**.
3. To verify RabbitMQ is running, you can visit [http://localhost:15672](http://localhost:15672) (if the management plugin is enabled).

---

### Step 2: Run the Applications

#### 1. Run the REST API

1. Open a terminal in the `rest_api` folder.  
2. Build the project:
   ```bash
   dotnet build
   ```markdown
# Run the Application

```bash
dotnet run
```

Access the REST API at:  
[http://localhost:5190](http://localhost:5190)  

**Note:** Logs for intermediate operations will appear in the terminal.

---

# Run the Entities Generator

1. Open another terminal in the `pointgenerator` folder.
2. Build the project:

```bash
dotnet build
```

3. Run the application:

```bash
dotnet run
```

Access the Entities Generator at:  
[http://localhost:5290](http://localhost:5290)  

**Note:** Logs for intermediate operations will appear in the terminal.

---

# Run the Entities Presenter (Map Application)

1. Open a terminal in the `entities_presenter` folder.
2. Build the project:

```bash
dotnet build
```

3. Run the application:

```bash
dotnet run
```

Access the Entities Presenter at:  
[http://localhost:5236](http://localhost:5236)  

**Notes:**
- Logs for WebSocket connections and intermediate actions will appear in the terminal.
- To debug WebSocket connections, open your browser’s Developer Tools (press `F12`) and check the **Network** tab.

---

# Troubleshooting

## 1. RabbitMQ Not Found
- Make sure RabbitMQ is installed and running on port `5672`.
- If RabbitMQ is not running, the applications may fail to connect, and errors will appear in the terminal.

## 2. Port Already in Use or Changed
- If you need to change any of the default ports:
  1. Update the port in the `Properties/launchSettings.json` file of the respective project.
  2. Rebuild and rerun the project.

**Important:** If you change a port, you must also update it in the following files:
- `entities_createor > Pages > Index.cshtml` (line 44)
- `entities_presenter > Pages > Index.cshtml` (line 33)
```
