Feature: Add vehicle using API
	In order to register our vehicles
	As a staff
	I want to execute restful calls easily

@RunServicesLocally
Scenario: Vehicle doesn't exists
	Given the API is running
	When a POST call is made to add new vehicle
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| Opala  | Chevrolet  | STANDARD | 4     | 5          | MANUAL       | 23      | 16       |
	Then the API status result is 200
	And the vehicle is in the database
	| Name   | Brand	  | Category | Doors | Passengers | Transmission | Consume | Emission |
	| Opala  | Chevrolet  | STANDARD | 4     | 5          | MANUAL       | 23      | 16       |