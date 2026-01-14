# Laboratory Work 7:

## 1. Project Overview

This release introduces a **Native Cross-Platform Mobile Application** developed using **.NET MAUI** (Multi-platform App UI). The application serves as a "thin client" for the Academic Tracker system, enabling users to access student data and statistics on both **Android** and **Windows** devices from a single codebase.

<img width="1904" height="986" alt="Pasted image 20260114182455" src="https://github.com/user-attachments/assets/4ef4b20f-cd21-4895-a309-c32ba07e5303" />

## 2. Architecture & Design Pattern

The application strictly follows the **MVVM (Model-View-ViewModel)** architectural pattern to ensure separation of concerns and testability:

- **Models:** `Student.cs` – Represents data structures mirroring the backend API.
    
- **Views (UI):** XAML-based pages (`StudentsPage`, `StatsPage`, `LoginPage`) defining the visual layout.
    
- **ViewModels:** `StudentsViewModel.cs` – Contains business logic, commands (e.g., `LoadStudentsCommand`), and observable properties (`IsBusy`, `Students`). Implements `INotifyPropertyChanged` for automatic UI updates.
    

## 3. Key Features Implementation

### 3.1. Authentication & Navigation

- **Login Screen:** Implemented a secure entry point (`LoginPage`) as per requirements.
    
- **Navigation:** Used **AppShell** for tab-based navigation between modules (Students, Statistics, About).
    

### 3.2. Data Consumption (Thin Client)

- **API Integration:** Developed `ApiService` to communicate with the REST backend (Lab 6).
    
- **Platform-Specific Networking:**
    
    - **Windows:** Connects to `localhost:7001`.
        
    - **Android:** Connects to `10.0.2.2:7001` (Android loopback address) to access the host machine's API.
        

### 3.3. User Experience (UX)

- **Animated Waiting Screen:** Implemented an asynchronous loading process with `ActivityIndicator`. The UI is locked, and a loading animation is displayed during data fetching (simulated 2s delay + API call time).
    
- **Data Visualization:** Integrated **Microcharts** library to render a bar chart visualizing student distribution by groups.
    

## 4. Cross-Platform Deployment

The application was successfully built and tested on two operating systems:

1. **Microsoft Windows 10/11:** Desktop experience using WinUI 3.
    
2. **Android (API 33/34):** Mobile experience via Android Emulator.
    

## 5. How to Run

1. **Prerequisites:**
    
    - Start the Backend API (Lab 6) on port `7001`.
        
    - Ensure Android Emulator is configured in Visual Studio.
        
2. **Launch:**
    
    - Select framework (`net8.0-windows` or `net8.0-android`).
        
    - Press **Run**.
        
3. **Usage:**
    
    - Enter credentials on the Login screen.
        
    - Navigate to "Students" and tap **"Load Data"** to test the API and waiting animation.
        
    - Navigate to "Stats" to view the performance graph.
