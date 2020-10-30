Feature: QueriesUsingApi
	In order to retrieve data from server
	As a user
	I want to execute restful calls easily

@RunServicesLocally
Scenario: Storage contains data
	Given the API is running
	When a POST call is made to add new vehicle
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |
	Then the API status result is 200
	And the API POST response has the following result
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |
	When a GET call is made with the following parameters
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |
	Then the API status result is 200
	And the API GET response has the following result
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |