Feature: CreateUser
	As a customer care specialist
	I want to create a new user profile
	So that I can register a new customer

@SmokeTest
Scenario Outline: Create new user
	Given I populate the API call
	When I make the API call to create a new user
	Then the call is successful
	And the user profile is created