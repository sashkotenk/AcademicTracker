# Laboratory Work 5:

## 1. Project Overview

This release extends the existing MVC application into a hybrid system with a fully functional **RESTful API**. The update focuses on exposing database entities via HTTP endpoints, implementing **API Versioning**, generating automatic documentation using **Swagger/OpenAPI**, and containerizing database infrastructure using **Docker Compose**.

## 2. Technology Stack Updates

- **API Framework:** ASP.NET Core Web API
    
- **Versioning:** `Microsoft.AspNetCore.Mvc.Versioning` (URL Segment based)
    
- **Documentation:** `Swashbuckle.AspNetCore` (Swagger UI)
    
- **Orchestration:** Docker Compose (v3.8)
    
- **Integration Testing:** Node.js, Jest, Supertest
    

---

## 3. RESTful API Architecture

The system implements a REST API to expose student data with support for multiple versions to ensure backward compatibility.

### Versioning Strategy

API versioning is implemented via **URL Path Versioning** (`/api/v{version}/...`).

#### **Version 1.0 (Legacy Support)**

- **Endpoint:** `GET /api/v1/students`
    
- **Controller:** `StudentsV1Controller`
    
- **Response Structure:** Returns a simplified JSON object containing only essential identification data.
    
    JSON
    
    ```
    { "id": 1, "fullName": "Ivan Petrenko", "group": "IPZ-31" }
    ```
    

#### **Version 2.0 (Enhanced Features)**

- **Endpoint:** `GET /api/v2/students`
    
- **Controller:** `StudentsV2Controller`
    
- **Logic:** Performs complex server-side calculations (Average Score) and returns extended profile data.
    
- **Response Structure:**
    
    JSON
    
    ```
    { "id": 1, "fullName": "...", "email": "...", "averageScore": 92.5 }
    ```
    

### API Documentation (Swagger)

The system integrates **Swagger UI** to visualize and interact with the API resources without access to the source code.

- **Access Path:** `/swagger`
    
- **Features:**
    
    - Interactive testing of endpoints ("Try it out").
        
    - Schema definitions for V1 and V2 models.
        
    - Automatic discovery of controllers via `AddSwaggerGen`.
        

---

## 4. Infrastructure & Containerization

To fulfill the multi-DBMS requirement, the project utilizes **Docker Compose** to orchestrate database services.

**File:** `docker-compose.yml`

- **Service 1: PostgreSQL**
    
    - Image: `postgres:latest`
        
    - Port Mapping: `5432:5432`
        
    - Persistence: Named volume `postgres-data`.
        
- **Service 2: MS SQL Server**
    
    - Image: `mcr.microsoft.com/mssql/server:2022-latest`
        
    - Port Mapping: `1433:1433`
        
    - Persistence: Named volume `mssql-data`.
        

This setup allows instant switching between database providers in `appsettings.json` without installing local SQL instances.

---

## 5. Integration Testing Strategy

Automated integration tests are implemented using the **Jest** framework to verify system behavior from an external client perspective (Black-box testing).

**Test Scope (`Tests/api.test.js`):**

1. **Status Code Verification:** Ensures all endpoints return `200 OK`.
    
2. **Structure Validation:**
    
    - Asserts that V1 response _excludes_ sensitive fields (Email).
        
    - Asserts that V2 response _includes_ calculated fields (AverageScore).
        
3. **Cross-Database Verification:** The test suite is designed to validate API consistency regardless of the active database provider (SQLite, Postgres, InMemory).
    

---

## 6. How to Run & Verify

### Step 1: Start Infrastructure

Launch the database containers using Docker Compose:

Bash

```
docker-compose up -d
```

### Step 2: Run Application

Start the .NET backend:

Bash

```
cd Tracker.Web
dotnet run
```

- **UI Demo:** Navigate to `https://localhost:7001/Demo` to see the AJAX-based interaction with both API versions.
    
- **Swagger:** Navigate to `https://localhost:7001/swagger` to inspect the API contract.
    

### Step 3: Execute Tests

Run the integration test suite:

Bash

```
cd Tests
npm test
```
