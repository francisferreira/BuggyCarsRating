Feature: Vote & Comment
	Test vote & comment functionality

@loggedout
Scenario: Vote Denied When Logged Out
	Given the "model" page is displayed
	 Then the vote cannot be cast
	  And the page says "You need to be logged in to vote."

@loggedin
Scenario: Vote Denied When Already Cast
	Given the "model" page is displayed
	 Then the vote cannot be cast
	  And the page says "Thank you for your vote!"

@uniqueid
Scenario: Vote Allowed Without Comment
	Given the "model" page is displayed
	  And the "first comment date" is saved
	  And the "number of comments" is saved
	  And the "number of votes" is saved
	 When vote is cast without comment
	 Then the page says "Thank you for your vote!"
	  And the "first comment date" remains unaltered
	  And the "number of comments" remains unaltered
	  And the "number of votes" increases by one

@uniqueid
Scenario: Vote Allowed With Comment
	Given the "model" page is displayed
	  And the "number of comments" is saved
	  And the "number of votes" is saved
	 When vote is cast with comment "My VW Beetle is faster"
	 Then the page says "Thank you for your vote!"
	  And the first comment "date" is correct
	  And the first comment "author" is correct
	  And the first comment "contents" are correct
	  And the "number of comments" increases by one
	  And the "number of votes" increases by one