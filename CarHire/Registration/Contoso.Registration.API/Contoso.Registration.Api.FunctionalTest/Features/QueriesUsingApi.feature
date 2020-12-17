Feature: QueriesUsingApi
	In order to retrieve data from server
	As a user
	I want to execute restful calls easily

@RunServicesLocally
Scenario: Storage contains data
	Given the API is running
	And user is authenticated
	When a POST call is made to add new vehicle
	| Name	| Brand		| Category	| Doors		| Passengers	| Transmission	| Consume	| Emission	|
	| F50		| Ferrari		| SPORT    | 2			| 2				| MANUAL	| 23				| 16				|
	Then the API Post status result is 200
	And the API Post response has the following result
	| Name	| Brand	  | Category	| Doors		| Passengers | Transmission | Consume	| Emission	|
	| F50		| Ferrari   | SPORT	| 2			| 2				| MANUAL  | 23				| 16				|
	When a GET call is made with the following parameters
	| Name  | Brand		| Category	| Doors | Passengers | Transmission		| Consume	| Emission	|
	| F50    | Ferrari    | SPORT    | 2		 | 2				 | MANUAL     | 23				| 16				|
	Then the API Get status result is 200
	And the API GET response has the following result
	| Name	| Brand	  | Category	| Doors		| Passengers	| Transmission	| Consume	| Emission	|
	| F50		| Ferrari   | SPORT	| 2			| 2				| MANUAL	| 23				| 16				|
	And the API GET response has the following header result
	| Header				| Key		| Value		|
	| X-Pagination	| Total    | 1			|
	| X-Pagination	| Page		| 1			|
	| X-Pagination	| Limit    | 10			|

@RunServicesLocally
Scenario: Storage does not contains data
	Given the API is running
	And user is authenticated
	When a GET call is made with the following parameters
	| Name	| Brand	|
	| Uno		| Fiat		|
	Then the API Get status result is 404
	And the API GET response has the following header result
	| Header				| Key		| Value		|
	| X-Pagination	| Total    | 0			|
	| X-Pagination	| Page		| 1			|
	| X-Pagination	| Limit    | 10			|

@RunServicesLocally
Scenario: User has no permission
	Given the API is running
	And user has no permission
	When a GET call is made with the following parameters
	| Name	| Brand	|
	| Uno		| Fiat		|
	Then the API Get status result is 401