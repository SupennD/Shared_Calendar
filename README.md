# Shared Calendar 

**Shared Calendar** is built with **C# Blazor** for the client-side, a **.NET Web API** for the backend server, and **Entity Framework Core (EFC)** with **SQLite** as the database. This project allows users to manage events within groups through a shared calendar. Users can create and join groups, view group-specific events on a calendar, and navigate between months to see scheduled activities.

## Features
- User authentication (login and registration)
- Shared calendar with month navigation to display events
- Group management (create and join groups)
- Event management (create events)

## Tech Stack
- **Client**: Blazor (C#)
- **Backend**: .NET 8.0 Web API
- **Database**: Entity Framework Core with SQLite

## Project Description
The Shared Calendar allows users to join groups and collaborate by sharing events in an organized calendar interface.

- Users must log in to access features.
- The calendar shows events based on the groups the user has joined.
- Backend API handles CRUD operations for events and groups with proper authentication.

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/supendra-shared-calendar.git

2. Open the solution in your IDE (e.g., Visual Studio, Rider).
3. Run the backend and frontend projects.
4. Access the application in your browser.

## Pages
- **Home**: Login/Register and navigate to Calendar or Groups
- **Calendar**: View and manage events for the groups you’ve joined
- **Groups**: Join, create, or manage groups

## License
This project is open-source. Feel free to contribute or modify it as needed.  
