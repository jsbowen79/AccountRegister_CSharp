# Overview

This was an attempt to convert a TypeScript code project to C# while adding functionality to create a fully functional console application.

The application provides the user with the ability to track financial transactions. The C# version is fully console operated and can be used by the user to keep a check register. Functionalit added
includes the ability for a user to use the program, the ability to load and utilize multiple accounts at the same time. Programmatic differentiation between account types, transaction list summary, and
centralized account management.

My intent is to eventually create a web-browser based application that can be used to track personal financial information.

{Provide a link to your YouTube demonstration. It should be a 4-5 minute demo of the software running and a walkthrough of the code. Focus should be on sharing what you learned about the language syntax.}

[Software Demo Video] https://www.youtube.com/watch?v=2t1J6uXB9F0

# Development Environment

The program was developed using VS Code as the development environment. I relies on the .NET SDK 10.0.108 to compile and run the application. It was written in C# utilizing Object-Oriented Design principles.
It utilizes Enums, lists, recursion, JSON serialization/deserialization, structures, loops, variables, and expressions. It has a menu driven CLI interface. It incorporates Data Transfer Objects to assist
with serialization/deserialization. It utilizes principles of inheritance, abstract classes, Polymorphism, and Application State management. Version control was provided by Git and GitHub. This was all
completed on a local computer running Windows 11 and Powershell terminal.

The programming Language was C# 14. I used the System.Text.Json and System.IO Libraries.

# Useful Websites

{Make a list of websites that you found helpful in this project}

- [Microsoft Learn C#](https://learn.microsoft.com/en-us/training/paths/get-started-c-sharp-part-1)
- [C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp)
- [.NET CLI Documentation](https://learn.microsoft.com/en-us/dotnet/core/tools)

# Future Work

{Make a list of things that you need to fix, improve, and add in the future.}

- The program currently assigns and tracks AcctNumber unique identifiers globally through the ApplicationStatus class, but transactionId identifiers are handled locally in the transaction class. I need
  to move this function to the global ApplicationStatus class so that transactionId's cannot repeat in different accounts.
- CategoryNodes are present and working, but as of now, they are unwieldy to use in a Console application; so every transaction is set to "Uncategorized" with no user intervention possible. Furthermore,
  there is currently no logic present for utilizing the CategoryNodes for tracking spending habits. I need to create a useable user interface for the CategoryNodes and develop the associated logic structure.
- The ultimate purpose is to create a graphical UI for this project. This needs to be accomplished.
