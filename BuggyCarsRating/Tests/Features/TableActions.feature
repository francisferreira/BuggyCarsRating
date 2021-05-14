Feature: Table Actions
	Test table actions functionality 

Scenario: Browse Table Forward In Page
	Given the "<page>" page is displayed
	 When the mid-page is reached via textbox
	 Then the expected page is displayed
	 When the "next" page button is clicked
	 Then the page number "increases" by one
	Given the "last" page is reached this way
	 When the "next" page button is clicked
	 Then the page number "remains" as is
Examples:
		  | page    |
		  | Maker   |
		  | Overall |

Scenario: Browse Table Backward In Page
	Given the "<page>" page is displayed
	 When the mid-page is reached via textbox
	 Then the expected page is displayed
	 When the "previous" page button is clicked
	 Then the page number "decreases" by one
	Given the "first" page is reached this way
	 When the "previous" page button is clicked
	 Then the page number "remains" as is
Examples:
		  | page    |
		  | Maker   |
		  | Overall |