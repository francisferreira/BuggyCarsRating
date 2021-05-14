@topvoted
Feature: Vote & Comment
	Test vote & comment functionality

@loggedout
Scenario: Vote Denied When Logged Out
	Given the "model" page is displayed
	 Then the user cannot cast their vote
	  And "You need to be logged in to vote." is shown

@loggedin
Scenario: Vote Denied When Already Cast
	Given the "model" page is displayed
	 Then the user cannot cast their vote
	  And "Thank you for your vote!" is shown

@loggedin
Scenario: Vote Allowed Without Comment
	Given the model rank #7's page is displayed
	  And the "first comment date" is saved
	  And the "number of comments" is saved
	  And the "number of votes" is saved
	 When vote is cast without comment
	 Then "Thank you for your vote!" is shown
	  And the "first comment date" remains unaltered
	  And the "number of comments" remains unaltered
	  And the "number of votes" increases by one

@loggedin
Scenario: Vote Allowed With Comment
	Given the model rank #8's page is displayed
	  And the "number of comments" is saved
	  And the "number of votes" is saved
	 When vote is cast with comment "Bite my shiny metal car!"
	 Then "Thank you for your vote!" is shown
	  And the first comment "date" is correct
	  And the first comment "author" is correct
	  And the first comment "contents" are correct
	  And the "number of comments" increases by one
	  And the "number of votes" increases by one