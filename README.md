# HangfireDemo

# Job Status Application

## Abstract

The Job Status Application addresses the need for real-time monitoring and management of job executions in a distributed system. It provides a user-friendly interface that displays live updates of job statuses, enhancing visibility and control over ongoing operations.

## Table of Contents

1. [Introduction](#introduction)
2. [Architecture Overview](#architecture-overview)
   - [Components](#components)
   - [Technologies Used](#technologies-used)
3. [Functionality](#functionality)
   - [Job Execution Flow](#job-execution-flow)
   - [Error Handling](#error-handling)
4. [Data Flow](#data-flow)
5. [Setup Instructions](#setup-instructions)
   - [Prerequisites](#prerequisites)
   - [Installation and Configuration](#installation-and-configuration)
6. [Conclusion](#conclusion)
7. [Future Enhancements](#future-enhancements)


## Introduction

The Job Status Application is designed to provide real-time updates on job execution status via a web interface. This document outlines the architecture, functionality, and setup instructions for the application using Angular as the frontend framework.

## Architecture Overview

### Components

1. **Hosted Service**:
   - Executes jobs on demand and scheduled using Hangfire.
   - Manages job execution and status.

2. **Backend-for-Frontend (BFF) Service**:
   - Provides API endpoints for the Angular frontend.
   - Enqueues on-demand jobs in the Hosted Service via API calls.

3. **Frontend (Angular)**:
   - Displays job status updates to users in real-time.
   - Utilizes SignalR for WebSocket-based communication with the server for live updates.

### Technologies Used

- **Backend**:
  - Hangfire for job scheduling and execution.
  - ASP.NET Core for backend services.
  - RESTful API endpoints for communication with the frontend.

- **Frontend**:
  - Angular framework for building the user interface.
  - SignalR for WebSocket-based communication with the backend.
  - HttpClientModule for making HTTP requests to the BFF service.

## Functionality

### Job Execution Flow

1. **Starting a Job**:
   - User triggers job execution from the Angular frontend by clicking on the "Start Job" button.
   - Frontend sends a request to the BFF service's API endpoint to start the job.

2. **Job Execution Process**:
   - BFF service enqueues the job request to the Hosted Service using API calls.
   - Hosted Service executes the job asynchronously.
   - During job execution, status updates are pushed to the frontend via SignalR.

### Error Handling

- Error handling is implemented at multiple levels:
  - Angular frontend displays error messages if job start or updates fail.
  - Backend logs errors and provides appropriate HTTP responses.

## Data Flow

### Flow of Data

1. **Initiation (Angular to BFF)**:
   - The user initiates a job from the Angular frontend by clicking the "Start Job" button.
   - Angular sends a POST request to the BFF service's API endpoint to start the job.

2. **Job Enqueue (BFF to Hosted Service)**:
   - The BFF service receives the request and enqueues the job by calling the Hosted Service's API endpoint.
   - The BFF service sends a response back to the Angular frontend confirming that the job has been enqueued.

3. **Job Execution (Hosted Service)**:
   - The Hosted Service picks up the job from the queue and begins execution.
   - The Hosted Service periodically sends status updates to the BFF service using SignalR.

4. **Real-Time Updates (BFF to Angular)**:
   - The BFF service forwards the job status updates to the Angular frontend using SignalR.
   - The Angular app receives these updates and dynamically updates the UI to reflect the current status of the job.

5. **Completion (Hosted Service to BFF to Angular)**:
   - Upon job completion, the Hosted Service sends a final status update to the BFF service.
   - The BFF service forwards this final update to the Angular frontend, which displays the completion status to the user.


## Setup Instructions

### Prerequisites

- Node.js and npm installed on your development machine.
- Visual Studio or Visual Studio Code for backend development.
- Basic understanding of ASP.NET Core, Angular, and SignalR.

### Installation and Configuration

1. **Backend Setup**:
   - Clone the backend repository (`link_to_backend_repo`).
   - Open the solution in Visual Studio.
   - Configure database connections and dependencies.
   - Run migrations (if applicable) and start the backend server.

2. **Frontend Setup**:
   - Clone the frontend repository (`link_to_frontend_repo`).
   - Navigate to the project directory.
   - Install dependencies with `npm install`.
   - Start the development server with `ng serve`.

3. **Configuration**:
   - Update configuration files (`appsettings.json`, `environment.ts`) with appropriate URLs and settings for backend APIs and SignalR Hub.

## Conclusion

The Job Status Application provides a robust solution for monitoring and managing job executions in real-time using Angular as the frontend framework. By leveraging Hangfire for job scheduling, SignalR for WebSocket-based communication, and Angular for building a responsive frontend, the application ensures efficient job management and user interaction.

## Future Enhancements

- Implement user authentication and authorization for secure access to job status.
- Enhance UI/UX with additional features such as job history, filtering, and sorting.
- Integrate logging and analytics for performance monitoring and troubleshooting.
