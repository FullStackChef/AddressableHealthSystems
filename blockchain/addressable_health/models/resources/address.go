package resources

// Address defines a FHIR Address structure
type Address struct {
    Line       []string `json:"line"`
    City       string   `json:"city"`
    State      string   `json:"state"`
    PostalCode string   `json:"postalCode"`
    Country    string   `json:"country"`
}
