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
	Then the API Post status result is 200
	And the API Post response has the following result
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |
	When a GET call is made with the following parameters
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |
	Then the API Get status result is 200
	And the API GET response has the following result
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| F50    | Ferrari    | SPORT    | 2     | 2          | MANUAL       | 23      | 16       |

Scenario: Storage does not contains data
	Given the API is running
	When a GET call is made with the following parameters
	| Name | Brand |
	| Uno  | Fiat  |
	Then the API Get status result is 404