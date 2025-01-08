
# Appointment Management API

This is an API for managing appointments in a medical system. It allows users to register, authenticate, and manage appointments with doctors. The API provides JWT-based authentication and CRUD operations for appointment management.

## Table of Contents
1. [Getting Started](#getting-started)
2. [API Endpoints](#api-endpoints)
    - [User Registration](#user-registration)
    - [User Login](#user-login)
    - [Create Appointment](#create-appointment)
    - [Get Appointments](#get-appointments)
    - [Update Appointment](#update-appointment)
    - [Delete Appointment](#delete-appointment)
3. [Testing the API](#testing-the-api)

## Getting Started

### Prerequisites

- .NET Core 6.0+ or later
- Postman or Swagger for testing API
- A valid database connection (SQL Server)
- Docker

### Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/REFATBHUYAN/AppointmentManagementAPI.git
   cd AppointmentManagementAPI
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Set up the database:
   - Create a local database or use a managed database (MSSQL Server).
   - Configure the connection string in the environment variable `ConnectionStrings__DefaultConnection`.

4. Apply migrations (if necessary):
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5001` by default.

## API Endpoints

### User Registration

- **POST /api/auth/register**
  
  Registers a new user.

  **Request Body:**
  ```json
  {
    "username": "user1",
    "password": "Password123",
    "email": "user1@example.com"
  }
  ```

  **Response:**
  - **201 Created** if successful.
  - **400 Bad Request** if the data is invalid.

### User Login

- **POST /api/auth/login**
  
  Logs in an existing user and returns a JWT token.

  **Request Body:**
  ```json
  {
    "username": "user1",
    "password": "Password123"
  }
  ```

  **Response:**
  - **200 OK** with a JWT token in the response body.
  - **400 Bad Request** if credentials are incorrect.

  Example response:
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
  ```

### Create Appointment

- **POST /api/appointments**

  Creates a new appointment.

  **Headers:**
  - `Authorization`: Bearer {JWT token}

  **Request Body:**
  ```json
  {
    "doctorId": 1,
    "userId": 1,
    "appointmentDate": "2025-01-10T15:00:00",
    "description": "Consultation for fever"
  }
  ```

  **Response:**
  - **201 Created** with the appointment details if successful.
  - **400 Bad Request** if data is invalid.

### Get Appointments

- **GET /api/appointments**

  Retrieves a list of all appointments.

  **Headers:**
  - `Authorization`: Bearer {JWT token}

  **Response:**
  - **200 OK** with the list of appointments.

### Update Appointment

- **PUT /api/appointments/{id}**

  Updates an existing appointment by ID.

  **Headers:**
  - `Authorization`: Bearer {JWT token}

  **Request Body:**
  ```json
  {
    "doctorId": 2,
    "userId": 1,
    "appointmentDate": "2025-01-12T16:00:00",
    "description": "Consultation for a headache"
  }
  ```

  **Response:**
  - **200 OK** if successful.
  - **404 Not Found** if the appointment with the given ID does not exist.

### Delete Appointment

- **DELETE /api/appointments/{id}**

  Deletes an appointment by ID.

  **Headers:**
  - `Authorization`: Bearer {JWT token}

  **Response:**
  - **200 OK** if successful.
  - **404 Not Found** if the appointment with the given ID does not exist.


## Testing the API

### Using Postman

1. **Test API Endpoints**:
   - **Register User**: Test the `/api/auth/register` endpoint with the user registration details.
   - **Login User**: Test the `/api/auth/login` endpoint to get the JWT token.
   - **Create Appointment**: Test the `/api/appointments` endpoint with the appropriate data.
   - **Get Appointments**: Test the `/api/appointments` endpoint to retrieve appointments.
   - **Update Appointment**: Test the `/api/appointments/{id}` endpoint to update an existing appointment.
   - **Delete Appointment**: Test the `/api/appointments/{id}` endpoint to delete an appointment.

### Using Swagger

1. Run your application locally.
2. Open your browser and navigate to `http://localhost:5001/swagger` (or the URL for your Swagger UI).
3. Use the Swagger UI to test the endpoints by sending requests directly from the browser.

## Troubleshooting

### Common Errors

1. **"Unauthorized"**: 
   - Ensure that you provide a valid JWT token in the `Authorization` header.
   - If the token is expired, request a new one via the login endpoint.

2. **Database connection errors**:
   - Ensure that the connection string is set correctly in the environment variables.
   - Make sure that the database is running and accessible.

3. **"Not Found" (404)**:
   - Ensure that the resource you are trying to access (e.g., appointment by ID) exists in the database.
