# Laboratory Work 8:

## 1. Project Overview

**Topic:** Integration of Modern Frontend Frameworks.

**Goal:** To expand the "Academic Tracker" ecosystem by implementing a Single Page Application (SPA) web client using the **React** framework. This client serves as a modern web interface for the existing .NET Web API.

## 2. Technology Stack

- **Framework:** React 18 (Functional Components + Hooks).
    
- **Language:** TypeScript (for type safety and integration with backend models).
    
- **Build Tool:** Vite (for fast hot-module replacement and build optimization).
    
- **HTTP Client:** Axios (for communicating with the .NET REST API).
    
- **Styling:** Bootstrap 5 / CSS Modules.
    

## 3. Architecture Changes

The system architecture has been upgraded to a **Headless** model:

- **Backend:** The existing .NET API (`Tracker.API`) now acts purely as a data provider.
    
- **Frontend:** A new independent project `Tracker.WebClient` handles the UI/UX logic.
    
- **Communication:** The React client consumes data via HTTP/JSON.
    
- **CORS Configuration:** Updated the ASP.NET Core backend to allow Cross-Origin Resource Sharing (CORS) requests from the React development server (`http://localhost:5173`).
    

## 4. Implementation Details

### 4.1. Project Structure

The frontend project structure follows standard React best practices:

Plaintext

```
src/
├── components/       # Reusable UI components (Navbar, Loader)
├── services/         # API communication logic (ApiService.ts)
├── models/           # TypeScript interfaces (Student.ts)
├── pages/            # Main views (HomePage, StudentsPage)
└── App.tsx           # Main application entry point with Routing
```

### 4.2. Key Features Implemented

1. **Student List Visualization:**
    
    - Created a `StudentsPage` component that fetches data using `useEffect`.
        
    - Implemented a stateful variable `students` using `useState` to store the API response.
        
    - Displayed data in a responsive Bootstrap table.
        
2. **Asynchronous Data Fetching:**
    
    - Implemented an asynchronous service layer using `Axios`.
        
    - Added error handling (try-catch blocks) to manage API connectivity issues.
        
    - **Loading State:** Integrated a visual spinner while data is being fetched from the backend.
        
3. **Cross-Platform Web Interface:**
    
    - The web client works identically on Windows, macOS, and Linux browsers, fulfilling the cross-platform requirement.
        

## 5. Backend Adjustments (CORS)

To enable the React app to talk to the .NET API, the following policy was added to `Program.cs` in the API project:

C#

```
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// ...
app.UseCors("AllowReactApp");
```

## 6. Deployment & Version Control

- **Git:** The complete source code, including the new `Tracker.WebClient` directory, has been committed and pushed to the remote repository.
    
- **Testing:** The application was verified on:
    
    - **OS:** Windows 11 (Host) and Linux (Ubuntu via Docker Container).
        
    - **Browsers:** Google Chrome and Mozilla Firefox.
        

## 7. How to Run

1. **Start the Backend API:**
    
    Bash
    
    ```
    cd Tracker.Web
    dotnet run
    ```
    
    _(Ensure API is running on https://localhost:7001)_
    
2. **Start the React Client:**
    
    Bash
    
    ```
    cd Tracker.WebClient
    npm install
    npm run dev
    ```
    
3. **Access:** Open `http://localhost:5173` in your browser.
