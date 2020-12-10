Feature: Add vehicle using API
	In order to register our vehicles
	As a user
	I want to execute restful calls easily

@RunServicesLocally
Scenario: Vehicle doesn't exists
	Given the API is running
	When a POST call is made to add new vehicle
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| Opala  | Chevrolet  | STANDARD | 2     | 4          | MANUAL       | 23      | 16       |
	Then the API Post status result is 200
	And the API Post response has the following result
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| Opala  | Chevrolet  | STANDARD | 2     | 4          | MANUAL       | 23      | 16       |
	And the vehicle is in the database
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| Opala  | Chevrolet  | STANDARD | 2     | 4          | MANUAL       | 23      | 16       |
	And the Following Integration Event Is Sent
	| Event							 |
	| VehicleCreatedIntegrationEvent |

@RunServicesLocally
Scenario: Vehicle sent with wrong parameters
	Given the API is running
	When a POST call is made to add new vehicle
	| Name			 | Brand		| Category	| Doors		| Passengers	| Transmission		| Consume	| Emission	|
	|					 | Chevrolet  | NA			| 4				| 5						| NA						| 23				| 16				|
	Then the API Post status result is 400
	And The API error response has the following result
	| StatusCode | Title										        | Detail																																|
	| 400				 | API Error. Please see the details. | 'Name' must not be empty. 'Category' must be valid. 'Transmission' must be valid.	|
	And no Integration Event is sent