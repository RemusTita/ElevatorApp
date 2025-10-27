Elevator Control System
A smart dual-elevator management system built with C# and Windows Forms, featuring modern design patterns and real-time animations.
Note: This is a university assignment project demonstrating software design patterns, Windows Forms development, and database integration.
Features

Two Elevators - Independent operation with smart dispatching
Smart Selection - Automatically picks the closest available elevator
Animated Movement - Smooth door and cabin animations
Live Status - Real-time floor indicators
Event Logging - SQLite database stores all operations

```

## How to Use

### Call an Elevator
- Click **Call Ground** or **Call First** to call an elevator
- The closest available elevator will respond

### Control Inside Elevator
- Each elevator has buttons **G** and **1**
- Click to move that specific elevator

### View History
- Click **View Database** to see all past events

## Project Structure


## Design Patterns Used

### State Pattern
Manages elevator behavior in different states:
- **Idle** - Waiting for requests
- **Moving** - Traveling between floors
- **Doors Opening** - Opening doors
- **Doors Open** - Waiting at floor
- **Doors Closing** - Closing doors

### Observer Pattern
MainWindow observes both elevators for automatic UI updates when:
- Elevator changes floor
- Elevator changes state
- Doors open/close

## Database

**Table: Events**
```
ID | Elevator   | Event   | Floor  | Time
-------------------------------------------
1  | Elevator A | Called  | Ground | 2025-01-15 14:30:22
2  | Elevator A | Arrived | Ground | 2025-01-15 14:30:28



Database file: ElevatorLog.db (created automatically in the application directory)


C# 12 with .NET 9.0
Windows Forms for UI
SQLite for data storage
BackgroundWorker for async operations
State & Observer design patterns

Assignment Requirements Met

 GUI with request buttons and control panels
 Floor selection and status displays
 Event logging with database storage
 Relative path configuration
 Exception handling throughout
 Animated elevator movement
 Multi-elevator support with smart dispatching
 State Design Pattern implementation
 Observer Design Pattern implementation
 BackgroundWorker for concurrent operations

Educational Objectives
This project demonstrates:

Advanced C# programming techniques
Design pattern implementation (State, Observer)
Asynchronous programming with BackgroundWorker
Database integration with SQLite
Event-driven architecture
UI/UX design principles
Exception handling best practices
Clean code and SOLID principles


Academic Integrity Note: This project is submitted as part of university coursework. Please respect academic integrity policies if you intend to reference or use this code for your own assignments.
