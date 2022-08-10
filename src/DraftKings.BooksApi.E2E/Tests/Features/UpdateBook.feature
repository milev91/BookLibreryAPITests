Feature: Update Book

Scenario: Update book positive
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	And I create an author
		| FirstName | LastName | DateOfBirth |
		| Jerome    | Salinger | 2/2/1902    |
	And I create a book
		| Title                  | Publisher | ReleaseDate |
		| The Catcher in the Rie | Rumen     | 2/2/1902    |
	When I update last created a book
		| Title                  | Publisher | ReleaseDate |
		| The Catcher in the Rye | Rumen     | 2/2/1902    |
	Then I verify that the book was updated

Scenario Outline: Update book negative
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	And I create an author
		| FirstName | LastName | DateOfBirth |
		| Jerome    | Salinger | 2/2/1902    |
	And I create a book
		| Title                  | Publisher | ReleaseDate |
		| The Catcher in the Rye | Rumen     | 2/2/1902    |
	When I update last created a book
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |
	Then I verify that the book was not updated

Examples:
	| Title                  | Publisher | ReleaseDate |
	|                        | Rumen     | 2/2/1902    |
	| null                   | Rumen     | 2/2/1902    |
	| The Catcher in the Rye | null      | 2/2/1902    |
	| The Catcher in the Rye |           | 2/2/1902    |