package base

import (
	"fmt"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

func (c *FhirContract) DeleteResource(ctx contractapi.TransactionContextInterface, key string) error {
	exists, err := c.resourceExists(ctx, key)
	if err != nil {
		return err
	}
	if !exists {
		return fmt.Errorf("resource with key %s does not exist", key)
	}
	var resource map[string]interface{}
	err = c.readFromLedger(ctx, key, &resource)
	if err != nil {
		return err
	}
	resource["deleted"] = true
	return c.writeToLedger(ctx, key, resource)
}
