Feature: Login & Logout
	Test login & logout functionality

@loggedin
Scenario: Login & Logout From Page - Profile
	Given the "profile" page is displayed
	 When "logout" is performed successfully
	 Then the login button is displayed
	 When "login" is performed successfully
	 Then the expected greeting is displayed

@loggedout
Scenario: Login & Logout From Page
	Given the "<page>" page is displayed
	 When "login" is performed successfully
	 Then the expected greeting is displayed
	 When "logout" is performed successfully
	 Then the login button is displayed
Examples:
		  | page     |
		  | Home     |
		  | Maker    |
		  | Model    |
		  | Overall  |
		  | Register |

Scenario: Login With Invalid Creds
	Given the "home" page is displayed
	 When logging in as "<username>" with "<password>"
	 Then "Invalid username/password" is displayed
Examples: 
		  | username | password |
		  | XX       | P4$$word |
		  | FF       | password |