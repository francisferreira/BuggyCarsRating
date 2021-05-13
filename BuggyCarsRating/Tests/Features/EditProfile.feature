@loggedin
Feature: Edit Profile
	Test edit profile functionality

Scenario: Change User Additional Info
	Given the "profile" page is displayed
	 When the additional info is changed as per app config
	  And the "Save" button is clicked
	 Then the "The profile has been saved" message pops up
	  And the new additional info is persisted

Scenario: Change User Password Info
	Given the "profile" page is displayed
	  And password is changed from "P4$$word" to "P@ssW0rd"
	 When the "Save" button is clicked
	  And "logout" is performed successfully
	 Then I "cannot" login using "P4$$word" as password
	  And I "can" login using "P@ssW0rd" as password
	Given the "profile" page is displayed
	  And password is changed from "P@ssW0rd" to "P4$$word"
	 When the "Save" button is clicked
	 Then the "The profile has been saved" message pops up

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
	  And the current age is recorded for reference
	 When the text "Thirty One" is entered for Age
	  And the "Save" button is clicked
	 Then the "Age is not valid" message pops up
	  And the age info remains unchanged

Scenario: Cancel User Profile Changes
	Given the "profile" page is displayed
	 When the basic info is edited as follows
		  | First Name | Last Name |
		  | Cancel     | This      |
	  And the "Cancel" button is clicked
	 Then the "home" page is displayed