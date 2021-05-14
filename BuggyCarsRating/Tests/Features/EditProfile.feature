@loggedin
Feature: Edit Profile
	Test edit profile functionality

Scenario: Change User Additional Info
	Given the "profile" page is displayed
	 When the additional info is edited
	  And the "Save" button is clicked
	 Then the "The profile has been saved" message pops up
	  And the edited additional info is persisted

Scenario: Change User Password Info
	Given the "profile" page is displayed
	  And the user password is changed
	 When the "Save" button is clicked
	 Then the "The profile has been saved" message pops up
	 When "logout" is performed successfully
	 Then login with the "old" password "fails"
	  And login with the "new" password "works"

Scenario: Alert Required Input
	Given the "profile" page is displayed
	 When the "<field>" field is deleted
	 Then the "<message>" message pops up
	  And the "Save" button is disabled
Examples:
		  | field      | message                |
		  | First Name | First Name is required |
		  | Last Name  | Last Name is required  |

Scenario: Alert Invalid Input - Age
	Given the "profile" page is displayed
	 When "Th!rty" is entered for Age
	  And the "Save" button is clicked
	 Then the "Age is not valid" message pops up
	  And the "age info" remains unchanged

Scenario: Cancel User Profile Changes
	Given the "profile" page is displayed
	 When the basic info is edited as follows
		  | First Name | Last Name |
		  | Cancel     | This      |
	  And the "Cancel" button is clicked
	 Then the "home" page is displayed
	  And the "basic info" remains unchanged