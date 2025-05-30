FROM ubuntu:20.04

# Set timezone non-interactively
ENV DEBIAN_FRONTEND=noninteractive

# Install basic packages
RUN apt-get update && apt-get install -y \
  curl \
  git \
  wget \
  nano \
  apt-transport-https \
  ca-certificates \
  gnupg \
  lsb-release \
  software-properties-common \
  tzdata \
  && apt-get clean && rm -rf /var/lib/apt/lists/*

# Set timezone to UTC to avoid interactive prompt
RUN ln -fs /usr/share/zoneinfo/Etc/UTC /etc/localtime && dpkg-reconfigure --frontend noninteractive tzdata

# Install Go
RUN wget https://golang.org/dl/go1.22.4.linux-amd64.tar.gz
RUN tar -C /usr/local -xzf go1.22.4.linux-amd64.tar.gz
ENV PATH=$PATH:/usr/local/go/bin

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_22.x | bash -
RUN apt-get update && apt-get install -y nodejs

# Install Docker Compose
RUN curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
RUN chmod +x /usr/local/bin/docker-compose

# Install Hyperledger Fabric binaries and Docker images
RUN curl -sSL https://bit.ly/2ysbOFE | bash -s
ENV PATH=$PATH:/fabric-samples/bin

# Set environment variables for Fabric
ENV FABRIC_CFG_PATH=/fabric-samples/config
ENV CORE_PEER_LOCALMSPID=Org1MSP
ENV CORE_PEER_TLS_ENABLED=true
ENV CORE_PEER_TLS_ROOTCERT_FILE=/fabric-samples/test-network/organizations/peerOrganizations/org1.example.com/peers/peer0.org1.example.com/tls/ca.crt
ENV CORE_PEER_MSPCONFIGPATH=/fabric-samples/test-network/organizations/peerOrganizations/org1.example.com/users/Admin@org1.example.com/msp
ENV CORE_PEER_ADDRESS=localhost:7051

# Set up working directory
WORKDIR /workspace

# Copy entrypoint script
COPY scripts/entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["entrypoint.sh"]
