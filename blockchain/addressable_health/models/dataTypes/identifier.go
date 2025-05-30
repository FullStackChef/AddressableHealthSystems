package dataTypes

// Identifier defines a FHIR Identifier structure
type Identifier struct {
	Use    string `json:"use"`    // usual | official | temp | secondary | old
	Type   string `json:"type"`   // e.g., MRN, SSN
	System string `json:"system"` // The namespace for the identifier value
	Value  string `json:"value"`  // The value that is unique
}
