package resources

import "addressableHealth/models/dataTypes"

// PatientResource represents a FHIR Patient resource
type PatientResource struct {
	ID         string                 `json:"id"`
	Identifier []dataTypes.Identifier `json:"identifier"`
	FirstName  string                 `json:"firstName"`
	LastName   string                 `json:"lastName"`
	Gender     string                 `json:"gender"`
	BirthDate  string                 `json:"birthDate"`
	Address    Address                `json:"address"`
}
