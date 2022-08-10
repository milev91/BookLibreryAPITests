Feature: Search Book

Scenario: Search book by id positive
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	And I create an author
		| FirstName         | LastName         | DateOfBirth         |
		| <AuthorFirstName> | <AuthorLastName> | <AuthorDateOfBirth> |
	And I create a book
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |
	When I search for last created book
	Then I verify that the book was retrieved
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |

Examples:
	| Title                  | Publisher | ReleaseDate | AuthorFirstName | AuthorLastName | AuthorDateOfBirth |
	| The Catcher in the Rye | Rumen     | 2/2/1902    | Jerome          | Salinger       | 2/2/1902          |


Scenario: Search book by id negative
	Given I register user with username "rumen", email "rument@gmail.com" and password "Asd123@@"
	And I login as "rument@gmail.com" with password "Asd123@@"
	And I create an author
		| FirstName         | LastName         | DateOfBirth         |
		| <AuthorFirstName> | <AuthorLastName> | <AuthorDateOfBirth> |
	And I create a book
		| Title   | Publisher   | ReleaseDate   |
		| <Title> | <Publisher> | <ReleaseDate> |
	When I search for non existing book
	Then I verify that the book was not retrieved

Examples:
	| Title                  | Publisher | ReleaseDate | AuthorFirstName | AuthorLastName | AuthorDateOfBirth |
	| The Catcher in the Rye | Rumen     | 2/2/1902    | Jerome          | Salinger       | 2/2/1902          |