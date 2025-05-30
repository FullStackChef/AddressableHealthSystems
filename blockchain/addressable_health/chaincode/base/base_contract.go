package base

import (
	"encoding/json"
	"fmt"
	"strings"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

type FhirContract struct {
	contractapi.Contract
}

func (c *FhirContract) writeToLedger(ctx contractapi.TransactionContextInterface, key string, data interface{}) error {
	dataJSON, err := json.Marshal(data)
	if err != nil {
		return fmt.Errorf("failed to marshal data: %v", err)
	}
	return ctx.GetStub().PutState(key, dataJSON)
}

func (c *FhirContract) readFromLedger(ctx contractapi.TransactionContextInterface, key string, out interface{}) error {
	dataJSON, err := ctx.GetStub().GetState(key)
	if err != nil {
		return fmt.Errorf("failed to read from ledger: %v", err)
	}
	if dataJSON == nil {
		return fmt.Errorf("data not found for key: %s", key)
	}
	return json.Unmarshal(dataJSON, out)
}

func (c *FhirContract) resourceExists(ctx contractapi.TransactionContextInterface, key string) (bool, error) {
	dataJSON, err := ctx.GetStub().GetState(key)
	if err != nil {
		return false, fmt.Errorf("failed to read from ledger: %v", err)
	}
	return dataJSON != nil, nil
}

func (c *FhirContract) buildQuery(criteria map[string]string) string {
	var queryBuilder strings.Builder
	queryBuilder.WriteString(`{"selector":{`)

	for key, value := range criteria {
		queryBuilder.WriteString(fmt.Sprintf(`"%s":{"$regex":"%s"},`, key, value))
	}

	query := queryBuilder.String()
	query = strings.TrimRight(query, ",") + `}}`
	return query
}
