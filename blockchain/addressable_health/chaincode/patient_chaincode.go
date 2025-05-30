package main

import (
	"addressableHealth/chaincode/base"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

type PatientResourceContract struct {
    base.FhirContract
}

// CreatePatientResource adds a new PatientResource to the world state
func (c *PatientResourceContract) CreatePatientResource(ctx contractapi.TransactionContextInterface, patientJSON string) error {
    var patient resources.PatientResource
    err := json.Unmarshal([]byte(patientJSON), &patient)
    if err != nil {
        return fmt.Errorf("failed to unmarshal patient JSON: %v", err)
    }
    return c.CreateResource(ctx, patient.ID, patientJSON)
}

// UpdatePatientResource updates an existing PatientResource in the world state
func (c *PatientResourceContract) UpdatePatientResource(ctx contractapi.TransactionContextInterface, patientJSON string) error {
    var patient resources.PatientResource
    err := json.Unmarshal([]byte(patientJSON), &patient)
    if err != nil {
        return fmt.Errorf("failed to unmarshal patient JSON: %v", err)
    }
    return c.UpdateResource(ctx, patient.ID, patientJSON)
}

// PatientResourceExists checks if a PatientResource exists in the ledger
func (c *PatientResourceContract) PatientResourceExists(ctx contractapi.TransactionContextInterface, id string) (bool, error) {
    return c.ResourceExists(ctx, id)
}

// ReadPatientResource retrieves a PatientResource from the world state
func (c *PatientResourceContract) ReadPatientResource(ctx contractapi.TransactionContextInterface, id string) (*resources.PatientResource, error) {
    resourceJSON, err := c.ReadResource(ctx, id)
    if err != nil {
        return nil, err
    }
    var patient resources.PatientResource
    err = json.Unmarshal([]byte(resourceJSON), &patient)
    if err != nil {
        return nil, fmt.Errorf("failed to unmarshal patient resource: %v", err)
    }
    return &patient, nil
}

// DeletePatientResource marks a PatientResource as deleted in the world state
func (c *PatientResourceContract) DeletePatientResource(ctx contractapi.TransactionContextInterface, id string) error {
    return c.DeleteResource(ctx, id)
}

// SearchPatientResources searches for PatientResources based on criteria
func (c *PatientResourceContract) SearchPatientResources(ctx contractapi.TransactionContextInterface, criteriaJSON string) ([]*resources.PatientResource, error) {
    resultsJSON, err := c.SearchResources(ctx, criteriaJSON)
    if err != nil {
        return nil, err
    }
    var patients []*resources.PatientResource
    err = json.Unmarshal([]byte(resultsJSON), &patients)
    if err != nil {
        return nil, fmt.Errorf("failed to unmarshal search results: %v", err)
    }
    return patients, nil
}

func main() {
    chaincode, err := contractapi.NewChaincode(new(PatientResourceContract))
    if err != nil {
        panic(err.Error())
    }

    if err := chaincode.Start(); err != nil {
        panic(err.Error())
    }
}
