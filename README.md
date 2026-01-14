# Laboratory Work 3: Academic Tracker

The application, titled **"Academic Tracker Pro"**, is an EdTech solution designed to monitor student performance in a distance learning environment. It allows for the registration of learners, management of their grades, and real-time calculation of group statistics.

<img width="1918" height="944" alt="Pasted image 20260114122958" src="https://github.com/user-attachments/assets/270e7f32-1f5a-4c11-9f01-fa114800aee0" />


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
    

## How to Test# Laboratory Work 1: Academic Tracker

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
