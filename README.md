# Laboratory Work 1: Academic Tracker

The application, titled **"Academic Tracker Pro"**, is an EdTech solution designed to monitor student performance in a distance learning environment. It allows for the registration of learners, management of their grades, and real-time calculation of group statistics.

## Technical Requirements Fulfillment

According to the assignment requirements:

- **Application Type**: Developed as a **.NET Core desktop application** (Windows Forms).
    
- **Testing**: Includes **unit tests** implemented with the **xUnit** framework to verify core business logic.
    
- **Architecture**: The solution is divided into three distinct projects to ensure cross-platform compatibility and separation of concerns:
    
    - `Tracker.Core`: A Class Library containing business entities and logic.
        
    - `Tracker.Desktop`: The graphical user interface (GUI) layer.
        
    - `Tracker.Tests`: The quality assurance layer for automated testing.
        

## Key Features

- **Learner Management**: Create and store student profiles with unique identifiers (GUIDs).
    
- **Grade Validation**: Ensures academic integrity by validating scores within a 0-100 range.
    
- **Analytical Statistics**: Automatically calculates the class average score using LINQ queries.
    
- **Modern UI**: Built with a responsive Windows Forms layout for enhanced user experience.
    

## How to Run

1. Navigate to the `Tracker.Desktop` directory.
    
2. Execute the command: `dotnet run`.
    

## How to Test

1. Navigate to the root solution directory.
    
2. Execute the command: `dotnet test`.


# Laboratory Work #2 

## 1. Project Overview

**Topic:** Object-Oriented Programming (OOP) and Class Library Development. **Goal:** To design and implement the core domain model (`Tracker.Core`) for the student performance monitoring system. The focus is on class structure, encapsulation, and relationships between entities.

## 2. Technical Architecture

The solution was refactored to separate business logic from the presentation layer.

- **Project Type:** .NET Class Library (`.dll`).
    
- **Namespace:** `Tracker.Core`.
    
- **Target Framework:** .NET 8.0.
    

## 3. Implementation Details

### 3.1. Domain Entities

Defined the fundamental data structures representing the academic process:

1. **Student Class**
    
    - **Responsibility:** Represents a single student entity.
        
    - **Properties:** `Id` (int), `FullName` (string), `Email` (string), `GroupId` (int).
        
    - **Encapsulation:** Implemented input validation in setters (e.g., ensuring `Email` is not empty).
        
2. **Group Class**
    
    - **Responsibility:** Represents an academic group containing multiple students.
        
    - **Properties:** `GroupId` (int), `GroupName` (string).
        
    - **Relationships:** Contains a collection `List<Student>` to manage students within the group.
        
3. **Journal/Subject Class**
    
    - **Responsibility:** Tracks grades and attendance.
        
    - **Logic:** Methods for calculating average scores and attendance percentage.
        

### 3.2. OOP Principles Applied

- **Encapsulation:** All class fields are private; access is provided via public Properties with validation logic.
    
- **Inheritance:** Created a base abstract class `Person` (containing `Name`, `Id`) which `Student` inherits from (if applicable in your specific code).
    
- **Polymorphism:** Overrode the `ToString()` method in the `Student` class to return formatted display strings (e.g., "Ivanenko I. - Group 1").
    

## 4. Key Algorithms & Logic

- **Data Validation:** Implemented checks to prevent invalid data entry (e.g., negative IDs or null names).
    
- **Collection Management:** Implemented methods to `Add`, `Remove`, and `Find` students within the `Group` class using LINQ.
    

## 5. Testing Results

- The class library was referenced by a Console Application (CLI).
    
- Verified successful object instantiation.
    
- Confirmed that validation logic correctly throws exceptions for invalid input.
    
- Verified that the `List<Student>` correctly stores and retrieves student objects.
    

## 6. Conclusion

The foundation of the "Academic Tracker" system has been established. The **Tracker.Core** library is now ready to be reused in future laboratory works (Web API, Mobile App) as a shared dependency.
