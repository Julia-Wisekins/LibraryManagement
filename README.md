# LibraryManagement
Please be advised that this application was developed as part of a programming test, with a primary focus on demonstrating 'Good' Programming Practices. As a result, the application has been intentionally designed to showcase these practices rather than prioritize functionality typical of a business-oriented application of this scale.

Contains:
* Dependency injection
* Encapsulation 
* Design patterns
* Unit Tests
* Comments

How to Run –
Open the solution file named LibraryManagement.sln and hit run.

Task –
1. Book Management:
* Implement functionality to add, update, and delete books.
* Each book should have at least the following properties: Title, Author, ISBN, and Availability
Status.
2. User Management:
* Implement functionality to add, update, and delete users.
*  Each user should have properties such as UserID, Name, and a list of borrowed books.
3. Borrowing and Returning Books:
* Implement functionality for a user to borrow a book.
* Ensure a book cannot be borrowed if it is not available.
* Implement functionality for a user to return a borrowed book.
4. Design Patterns and OO Practices:
* Use appropriate design patterns where necessary (e.g., Factory Pattern for creating books,
Singleton Pattern for the library manager).
* Follow object-oriented principles, such as encapsulation, inheritance, and polymorphism.
* Demonstrate the use of interfaces and abstract classes where applicable.
5. Error Handling and Validation:
* Implement robust error handling and input validation.
* Ensure that invalid operations (e.g., borrowing a book that is already borrowed) are
properly handled and communicated to the user.
6. Unit Tests:
* Write unit tests for the main functionalities using a testing framework like NUnit or MSTest.
* Ensure that the tests cover various edge cases and scenarios.  
Bonus: 
* Implement additional features such as account transfers or transaction history. 
* Add exception handling and validation to ensure robust input handling.

Assumptions – 
* Availability Consists of multiple states
* Books do not need a link to the borrower
* UI should only need the repository information

Review – 
Good Practice:
* The application is commented throughout.
* Design Patterns used: Singleton, Repository, Mediator
* Application is abstract and allows for changes to how the objects are managed and allows easy changes to where the objects are stored. Frontend could be changed easily.
* Has Descriptive Errors

Could be better:
* More testing could be added to the application. 
* No feedback on user end has been added.
* Errors Currently aren’t handled on the front end
