package base

import (
	"encoding/json"
	"fmt"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

func (c *FhirContract) ReadResource(ctx contractapi.TransactionContextInterface, key string) (string, error) {
	var resource map[string]interface{}
	err := c.readFromLedger(ctx, key, &resource)
	if err != nil {
		return "", fmt.Errorf("failed to read resource: %v", err)
	}
	if resource["deleted"].(bool) {
		return "", fmt.Errorf("resource with key %s is deleted", key)
	}
	resourceJSON, err := json.Marshal(resource)
	if err != nil {
		return "", fmt.Errorf("failed to marshal resource JSON: %v", err)
	}
	return string(resourceJSON), nil
}
