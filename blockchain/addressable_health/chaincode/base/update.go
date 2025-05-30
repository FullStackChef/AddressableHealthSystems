package base

import (
	"encoding/json"
	"fmt"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

func (c *FhirContract) UpdateResource(ctx contractapi.TransactionContextInterface, key string, resourceJSON string) error {
	exists, err := c.resourceExists(ctx, key)
	if err != nil {
		return err
	}
	if !exists {
		return fmt.Errorf("resource with key %s does not exist", key)
	}
	var resource map[string]interface{}
	err = json.Unmarshal([]byte(resourceJSON), &resource)
	if err != nil {
		return fmt.Errorf("failed to unmarshal resource JSON: %v", err)
	}
	resource["deleted"] = false
	return c.writeToLedger(ctx, key, resource)
}
