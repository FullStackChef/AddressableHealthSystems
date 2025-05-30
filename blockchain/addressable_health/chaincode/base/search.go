package base

import (
	"encoding/json"
	"fmt"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

func (c *FhirContract) SearchResources(ctx contractapi.TransactionContextInterface, criteriaJSON string) (string, error) {
	var criteria map[string]string
	err := json.Unmarshal([]byte(criteriaJSON), &criteria)
	if err != nil {
		return "", fmt.Errorf("failed to unmarshal search criteria JSON: %v", err)
	}
	criteria["deleted"] = "false" // Exclude deleted resources from search results
	queryString := c.buildQuery(criteria)
	resultsIterator, err := ctx.GetStub().GetQueryResult(queryString)
	if err != nil {
		return "", fmt.Errorf("failed to execute search query: %v", err)
	}
	defer resultsIterator.Close()

	var resources []map[string]interface{}
	for resultsIterator.HasNext() {
		queryResponse, err := resultsIterator.Next()
		if err != nil {
			return "", fmt.Errorf("failed to iterate through query results: %v", err)
		}

		var resource map[string]interface{}
		err = json.Unmarshal(queryResponse.Value, &resource)
		if err != nil {
			return "", fmt.Errorf("failed to unmarshal query result: %v", err)
		}

		resources = append(resources, resource)
	}

	resourcesJSON, err := json.Marshal(resources)
	if err != nil {
		return "", fmt.Errorf("failed to marshal resources JSON: %v", err)
	}
	return string(resourcesJSON), nil
}
