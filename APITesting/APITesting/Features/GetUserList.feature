Feature: GetUserList

	As a customer care specialist
	I want to get the list of user profiles
	So that I can see if any information needs to be updated

@SmokeTest
Scenario: Get User List
	Given I populate the API call to get a list of user
	When I receive the API call response with the list
	Then the list should contain info on all users
