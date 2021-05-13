@loggedout
Feature: Register User
	Test register user functionality

Scenario: Register User With Unique Id
	Given the "register" page is displayed
	  And the fields are filled as per app config
	  And the unique identifier is appended to login name
	 When the "Register" button is clicked
	 Then the "Registration is successful" message pops up
	  And login "can" be performed as expected

Scenario: Register User With Same Id
	Given the "register" page is displayed
	  And the fields are filled as per app config
	 When the "Register" button is clicked
	 Then the "User already exists" message pops up
	  And login "cannot" be performed as expected

Scenario: Cancel User Registration
	Given the "register" page is displayed
	  And the fields are filled as per app config
	 When the "Cancel" button is clicked
	 Then the "home" page is displayed

Scenario: Alert Required Input
	Given the "register" page is displayed
	  And the fields are filled as per app config
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