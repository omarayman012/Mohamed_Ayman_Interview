FastEndpointApi Project Explanation
Project Overview

This project is a .NET 8 Web API built using FastEndpoints following Clean Architecture principles.

The system manages:

Students
Classes
Enrollments
Marks
Reports

The application uses:

.NET 8
FastEndpoints
In-Memory Database using ConcurrentDictionary
DTOs
Service Layer
Pagination
Filtering
Clean Code Practices
Architecture Used

The project was divided into multiple layers to follow Separation of Concerns.

1. Domain Layer

Contains the core entities of the system.

Examples:

Student
Class
Enrollment
Mark

Example:

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

Purpose:

Represents database models.
Contains core business data.
2. Application Layer

Contains:

DTOs
Interfaces
Common Classes
DTOs

DTO stands for Data Transfer Object.

Used to:

Receive requests
Return responses
Avoid exposing entities directly

Examples:

CreateStudentRequest
StudentResponse
MarkListResponse
EnrollmentListResponse
Interfaces

Interfaces define contracts between layers.

Examples:

public interface IStudentService
{
    Result<StudentResponse> Create(CreateStudentRequest request);
}

Benefits:

Loose coupling
Easier testing
Better maintainability
Common Classes
Result

Used as a unified response wrapper.

Example:

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public T Data { get; set; }
}

Benefits:

Standardized API responses
Better error handling
Cleaner frontend integration
PaginatedList

Used for pagination.

Features:

PageNumber
TotalPages
TotalCount
HasPreviousPage
HasNextPage

Used in:

Students list
Classes list
Enrollments list
Marks list
3. Infrastructure Layer

Contains:

Services
FakeDb
Dependency Injection
FakeDb

The project uses in-memory storage instead of a real database.

We used:

ConcurrentDictionary<int, Student>

Benefits:

Fast development
Easy testing
No SQL setup needed

Collections used:

Students
Classes
Enrollments
Marks
Services

Business logic was implemented inside services.

Examples:

StudentService
ClassService
EnrollmentService
MarkService

Benefits:

Clean endpoints
Reusable business logic
Easier maintenance
4. API Layer

Contains FastEndpoints endpoints.

Each endpoint handles:

Request receiving
Calling service layer
Returning response

No business logic was written directly inside endpoints.

FastEndpoints

FastEndpoints is an alternative to Controllers in ASP.NET Core.

Benefits:

Faster
Cleaner
Minimal boilerplate
Better organization

Example:

public class CreateStudentEndpoint
    : Endpoint<CreateStudentRequest, Result<StudentResponse>>
Features Implemented
1. Students Module
Create Student
Endpoint
POST /students
Features
Create new student
Validation for:
FirstName
LastName
Age > 0
Response

Returns created student data.

Get All Students
Endpoint
GET /students
Features
Pagination
Filtering by:
Name
Age
Query Parameters
?page=1&pageSize=10&name=omar&age=20
Update Student
Endpoint
PUT /students
Features
Update student information
Validate student existence
Delete Student
Endpoint
DELETE /students/{id}
Features
Remove student from FakeDb
Return 404 if not found
Generate Student Report
Endpoint
GET /api/students/{id}/report
Features

Returns:

Student classes
Student marks
Overall average

Example Response:

{
  "studentName": "Omar Hassan",
  "average": 85
}
2. Classes Module
Create Class
Endpoint
POST /classes
Features
Create new class
Prevent duplicate class names
Get All Classes
Endpoint
GET /classes
Features
Pagination
Filtering by:
Name
Teacher
Delete Class
Endpoint
DELETE /classes/{id}
Average Marks For Class
Endpoint
GET /api/classes/{classId}/average-marks
Features

Calculates:

Average marks
Students count

Validation:

Class exists
Marks exist
3. Enrollment Module
Enroll Student
Endpoint
POST /enrollments
Features
Validate student exists
Validate class exists
Prevent duplicate enrollments
Save enrollment date
Get All Enrollments
Endpoint
GET /enrollments
Features
Pagination
Filtering by:
Student Name
Class Name
Special Design

Instead of returning:

{
  "studentId": 1,
  "classId": 2
}

We created a custom response:

{
  "studentName": "Omar Hassan",
  "className": "Math"
}

This improves readability.

Delete Enrollment
Endpoint
DELETE /enrollments/{id}
4. Marks Module
Record Marks
Endpoint
POST /marks
Features
Validate student exists
Validate class exists
Validate enrollment exists

Marks include:

ExamMark
AssignmentMark
TotalMark
Get All Marks
Endpoint
GET /marks
Features
Pagination
Filtering
Return student name and class name instead of IDs

Example:

{
  "studentName": "Sara Ali",
  "className": "Physics",
  "totalMark": 90
}
Delete Mark
Endpoint
DELETE /marks/{id}
Important Concepts Used
1. Pagination

Pagination improves API performance.

Instead of returning all records, we return small chunks.

Example:

?page=1&pageSize=5
2. Filtering

Filtering allows searching specific data.

Examples:

/students?name=omar
/classes?teacher=ahmed
3. Validation

Validation prevents invalid data.

Examples:

Age must be greater than zero
Student must exist
Class must exist
Prevent duplicate enrollment
4. Error Handling

Used consistent error responses.

Examples:

{
  "isSuccess": false,
  "message": "Student not found",
  "statusCode": 404
}
5. Mapping

We used mapping methods to convert Entities into DTOs.

Example:

private StudentResponse Map(Student s)

Benefits:

Cleaner responses
Better security
Separation between database and API response
6. ConcurrentDictionary

Used for thread-safe in-memory storage.

Benefits:

Safe for concurrent requests
Fast access
Suitable for in-memory APIs
Clean Code Practices Followed
Separation of Concerns
Layered Architecture
DTO Pattern
Service Layer Pattern
Reusable Methods
Clear Naming
Consistent Responses
Pagination Support
Filtering Support
Error Handling
Technologies Used
ASP.NET Core 8
FastEndpoints
C#
ConcurrentDictionary
Swagger
Dependency Injection
LINQ
Final Notes

This project demonstrates:

API Design
Clean Architecture
FastEndpoints Usage
Service Layer Implementation
Pagination and Filtering
DTO Usage
In-Memory Data Handling
Business Logic Separation
Professional API Structure

The project is scalable and can later be migrated from FakeDb to:

SQL Server
Entity Framework Core
Repository Pattern
Authentication & Authorization
CQRS
MediatR
AutoMapper

without changing the API structure significantly.
