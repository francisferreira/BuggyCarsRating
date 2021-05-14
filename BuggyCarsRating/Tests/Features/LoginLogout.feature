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

@loggedout
Scenario: Login With Invalid Creds
	Given the "home" page is displayed
	 When login is done with wrong "<inputs>"
	 Then "Invalid username/password" is displayed
Examples:
		  | inputs   |
		  | Username |
		  | Password |