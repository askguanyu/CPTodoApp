# Architecture Overview

## Purpose and Scope

CPTodoApp is a Windows application that provides clipboard monitoring and Sticky Notes integration functionality. The application operates as a background service with a Windows Forms-based management interface, following a layered architecture with clear separation between the application core, utilities, and external system interfaces.

## System Architecture Overview

The application is built on .NET Framework 4.7 and follows a layered architecture with these core components:

### Core Architecture Components

- **Program.cs** - Application entry point and single instance management
- **MainForm.cs** - Central UI controller and orchestrator
- **AppConstants.cs** - Centralized configuration constants
- **UILogger.cs** - Logging system

### Utilities Layer

- **GlobalHotKey.cs** - Global hotkey management (Ctrl+Alt+D)
- **ClipboardHelper.cs** - Text processing and clipboard operations
- **StickyNotesAutomation.cs** - Windows Sticky Notes integration
- **AutoStartHelper.cs** - Windows startup management

### Framework Dependencies

Key dependencies include:
- `System.Windows.Forms` for UI framework
- `UIAutomationClient` for Sticky Notes automation
- `IWshRuntimeLibrary` for Windows Shell integration

## Core Workflow

The application's main workflow centers around clipboard processing triggered by the global hotkey Ctrl+Alt+D:

1. User presses Ctrl+Alt+D hotkey
2. `WndProc` receives `WM_HOTKEY` message
3. `HandleClipboard()` method processes clipboard content
4. Text is cleaned and sent to Windows Sticky Notes
5. Clipboard is cleared after successful operation

## External Integration

The application integrates with Windows system services through:
- **Windows API** for global hotkey registration
- **System Clipboard** for text retrieval and manipulation
- **UI Automation Framework** for Sticky Notes control
- **Windows Shell** for startup shortcut management

**Notes**

The application uses a mutex-based single instance enforcement and operates primarily from the system tray. The architecture emphasizes minimal resource usage while providing seamless integration with Windows Sticky Notes through UI automation. All major components are compiled into the main executable with embedded resources for the UI.
