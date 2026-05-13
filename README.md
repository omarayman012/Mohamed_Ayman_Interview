11:00 AM → 11:15 AM

* Started the task.
* Planned the project structure using Clean Architecture.
* Began setting up FastEndpoints and organizing the solution layers.

11:15 AM → 11:25 AM

* Faced a configuration issue with FastEndpoints.
* The project setup was not working correctly, which delayed the start of development.

11:30 AM → 11:45 AM

* Searched for a ready FastEndpoints project setup on GitHub.
* Found a working project template.
* Customized and configured it to match the assignment requirements.

11:50 AM → 12:15 PM

* Started the actual implementation.
* Refactored the project structure because the template initially had everything in a single layer.
* Reorganized the code into separate layers:

  * Domain
  * Application
  * Infrastructure
  * API
* Implemented the first Student CRUD successfully.

12:15 PM → 1:00 PM

* Continued implementing the remaining modules and endpoints:

  * Students CRUD
  * Classes CRUD
  * Enrollments
  * Marks
* Added:

  * Pagination
  * Filtering
  * Validations
  * Error handling
* Implemented:

  * Student Report endpoint
  * Class Average Marks endpoint
* Used ConcurrentDictionary as in-memory storage.
* Tested endpoints using Swagger.
* One of the time-consuming parts was manually inserting test data into the in-memory collections during testing.

1:00 PM

* Finished the first complete version (Version 1) of the task.

1:15 PM → 1:30 PM

* Had a review and discussion session with Eng. Hassan.
* Discussed improvements and API response refinements.

1:30 PM → 2:00 PM

* Applied the requested refactoring and improvements.
* Updated API responses to return:

  * StudentName instead of StudentId
  * ClassName instead of ClassId
* Improved response DTOs and mapping methods.
* Cleaned and reorganized parts of the code.

2:00 PM → 2:30 PM

* Re-tested the project carefully.
* Reviewed the endpoints again to ensure everything was working correctly.
* Made final small adjustments and fixes before the meeting.

2:30 PM

* Joined the final meeting after successfully completing the task.

Technologies & Concepts Used:

* ASP.NET Core 8
* FastEndpoints
* Clean Architecture
* Dependency Injection
* ConcurrentDictionary
* LINQ
* Swagger
* Pagination & Filtering
* DTOs
* Service Layer Pattern
* Error Handling & Validation
