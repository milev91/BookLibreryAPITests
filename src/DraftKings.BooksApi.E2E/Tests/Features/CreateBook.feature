Feature: Create Book

Scenario Outline: Create book positive
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	When I create an author
		| FirstName         | LastName         | DateOfBirth         |
		| <AuthorFirstName> | <AuthorLastName> | <AuthorDateOfBirth> |
	And I create a book
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |
	Then I verify that the book was created

Examples:
	| Title                  | Publisher | ReleaseDate | AuthorFirstName | AuthorLastName | AuthorDateOfBirth |
	| The Catcher in the Rye | Rumen     | 2/2/1902    | Jerome          | Salinger       | 2/2/1902          |

Scenario Outline: Create book negative
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	When I create an author
		| FirstName         | LastName         | DateOfBirth         |
		| <AuthorFirstName> | <AuthorLastName> | <AuthorDateOfBirth> |
	And I create a book
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |
	Then I verify that the book was not created

Examples:
	| Title                  | Publisher | ReleaseDate | AuthorFirstName | AuthorLastName | AuthorDateOfBirth |
	| The Catcher in the Rye | Rumen     | 2/2/1902    |                 | Salinger       | 2/2/1902          |
	| The Catcher in the Rye | Rumen     | 2/2/1902    | Jerome          |                | 2/2/1902          |
	|                        | Rumen     | 2/2/1902    |                 | Salinger       | 2/2/1902          |
	| The Catcher in the Rye |           | 2/2/1902    |                 | Salinger       | 2/2/1902          |