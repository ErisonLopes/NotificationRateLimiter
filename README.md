# Rate-Limited Notification Service

## Project Description

This project implements a **Rate-Limited Notification Service**, which protects recipients from receiving excessive notifications by applying rate limits based on notification type. The solution was developed using **.NET Core** and follows **Clean Architecture** principles to promote a clear separation of concerns.

### Rate-Limiting Rules
Notifications have different rate-limiting rules, such as:

- **Status Updates**: No more than 2 per minute for each user.
- **News**: No more than 1 per day for each user.
- **Marketing**: No more than 3 per hour for each user.

If the number of sent notifications exceeds the configured limit, new notifications will be blocked until the specified period is restored.

## Architecture

The project is structured following **Clean Architecture** principles, which organizes the code into layers and promotes dependency inversion. The main layers are:

- **Domain**: Contains fundamental entities and interfaces, as well as business rules that do not depend on implementation details.
- **Application**: Contains services that orchestrate the application's behavior.
- **Infrastructure**: Implements the details of data access and external services, such as repositories and notification delivery services.
- **Api**: Exposes the API endpoints (in this case, an ASP.NET Core application).

## Running the Project via Docker

### Prerequisites

- **Docker** installed on your machine.

### Steps to Run the Project

1. **Clone the repository:**

   ```bash
   git clone https://github.com/ErisonLopes/NotificationRateLimiter.git
   cd NotificationRateLimite
2. **Build the project using Docker:**
    ```bash
   docker build -t notificationratelimiter-image --progress=plain --no-cache -f Dockerfile .
3. **Run the project:**
    ```bash
    docker run -p 8080:8080 -it --name notificationratelimiter-container notificationratelimiter-image
4. **Accessing the swagger**
    ```bash
    http://localhost:8080/swagger