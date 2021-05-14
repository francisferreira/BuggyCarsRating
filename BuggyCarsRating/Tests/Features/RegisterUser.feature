@loggedout
Feature: Register User
	Test register user functionality

Scenario: Register User With Unique Id
	Given the "register" page is displayed
	  And the registration form is filled out
	  And the login name "is" unique
	 When the "Register" button is clicked
	 Then the "Registration is successful" message pops up
	  And login "works" with those creds

Scenario: Register User With Same Id
	Given the "register" page is displayed
	  And the registration form is filled out
	  And the login name "is not" unique
	 When the "Register" button is clicked
	 Then the "User already exists" message pops up
	  And login "fails" with those creds

Scenario: Cancel User Registration
	Given the "register" page is displayed
	  And the registration form is filled out
	  And the login name "is" unique
	 When the "Cancel" button is clicked
	 Then the "home" page is displayed
	  And login "fails" with those creds

Scenario: Alert Required Input
	Given the "register" page is displayed
	  And the registration form is filled out
	 When the "<field>" field is cleared
	 Then the "<message>" message pops up
	  And the "Register" button is disabled
Examples:
		  | field            | message                |
		  | Login Name       | Login is required      |
		  | First Name       | First Name is required |
		  | Last Name        | Last Name is required  |
		  | Password         | Password is required   |
		  | Confirm Password | Passwords do not match |