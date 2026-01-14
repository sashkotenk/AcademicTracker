# Laboratory Work 4:

## 1. Project Overview

This application is a cross-platform web system built on **ASP.NET Core MVC**. It implements an Object-Relational Mapping (ORM) layer using **Entity Framework Core (EF Core)** with a **Code First** approach. The system supports dynamic switching between four different Database Management Systems (DBMS) and provides advanced search capabilities with complex data filtering.

## 2. Technology Stack

- **Framework:** .NET 8.0 (ASP.NET Core MVC)
    
- **ORM:** Entity Framework Core
    
- **Authentication:** OAuth2 (via Auth0)
    
- **Supported DBMS:**
    
    1. SQLite
        
    2. PostgreSQL
        
    3. Microsoft SQL Server
        
    4. In-Memory Database
        

---

## 3. Database Architecture (Data Models)

The database schema consists of three normalized entities with the following relationships:

### Entities

1. **Group**
    
    - Primary Key: `Id`
        
    - Fields: `Name`
        
    - Relationship: One-to-Many with `Student`.
        
2. **Student**
    
    - Primary Key: `Id`
        
    - Fields: `FullName`, `Email`.
        
    - Foreign Key: `GroupId`.
        
    - Relationship: Many-to-One with `Group`, One-to-Many with `Submission`.
        
3. **Submission** (Central Table)
    
    - Primary Key: `Id`
        
    - Fields: `CourseWorkTitle`, `Score`, `SubmissionDate`.
        
    - Foreign Key: `StudentId`.
        
    - Relationship: Many-to-One with `Student`.
        

---

## 4. Multi-DBMS Configuration

The application allows switching the underlying database provider without changing the code logic. This is implemented via the `appsettings.json` configuration file.

**Configuration Mechanism:** The `Program.cs` file reads the `Database:Provider` value and initializes the corresponding DbContext options using a switch-case logic.

**Supported Providers:**

- `"Sqlite"`: Uses `Microsoft.EntityFrameworkCore.Sqlite`.
    
- `"Postgres"`: Uses `Npgsql.EntityFrameworkCore.PostgreSQL`.
    
- `"SqlServer"`: Uses `Microsoft.EntityFrameworkCore.SqlServer`.
    
- `"InMemory"`: Uses `Microsoft.EntityFrameworkCore.InMemory`.
    

**Code First Implementation:** The `AppDbContext` class includes a data seeding method (`OnModelCreating`) that automatically populates the database with initial testing data upon application startup (`EnsureCreated`).
<img width="1077" height="426" alt="Pasted image 20260114134716" src="https://github.com/user-attachments/assets/c83900a2-028b-48c1-aeb8-4228a6c4a36a" />

---

## 5. Advanced Search Functionality

The `SearchController` implements a complex search query that filters the central table (`Submission`) based on multiple criteria.

### Query Logic

The search engine utilizes **LINQ** to build a dynamic SQL query. It performs **two JOIN operations** (Eager Loading) to retrieve related data:

1. `Submission` JOIN `Student`
    
2. `Student` JOIN `Group`
    

### Filter Specifications

1. **Text Search:** Filters records where:
    
    - `Student.FullName` **starts with** the query string.
        
    - OR `CourseWorkTitle` **ends with** the query string.
        
2. **Date Range:** Filters records by `SubmissionDate` (From/To).
    
3. **List Filtering:** Filters records by exact match of `GroupId` selected from a dropdown list.
    

---

<img width="1077" height="426" alt="Pasted image 20260114134716" src="https://github.com/user-attachments/assets/b1449c40-be8b-4ba5-9256-0b78be942752" />


## 6. How to Run

### Prerequisites

- .NET 8.0 SDK
    
- (Optional) Docker for running PostgreSQL or SQL Server instances.
    

### Setup Instructions

1. **Clone Repository:**
    
    Bash
    
    ```
    git clone <repository_url>
    cd AcademicTrackerLab/Tracker.Web
    ```
    
2. **Configure Database:** Open `appsettings.json` and set the desired provider:
    
    JSON
    
    ```
    "Database": {
      "Provider": "Sqlite", 
      "ConnectionStrings": { ... }
    }
    ```
    
3. **Run Application:**
    
    Bash
    
    ```
    dotnet run
    ```
