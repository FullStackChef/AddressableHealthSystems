package main

import (
	"addressableHealth/chaincode/base"

	"github.com/hyperledger/fabric-contract-api-go/contractapi"
)

type ObservationResourceContract struct {
	base.FhirContract
}

func main() {
	chaincode, err := contractapi.NewChaincode(new(ObservationResourceContract))
	if err != nil {
		panic(err.Error())
	}

	if err := chaincode.Start(); err != nil {
		panic(err.Error())
	}
}
