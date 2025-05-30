#!/bin/bash

# Move to fabric-samples directory
cd /fabric-samples/test-network

# Bring up the network and create the channel
./network.sh up createChannel -c mychannel -ca

# Deploy the chaincode
./network.sh deployCC -ccn basic -ccp ../asset-transfer-basic/chaincode-go -ccl go

# Keep the container running
tail -f /dev/null
