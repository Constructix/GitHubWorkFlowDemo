param subscriptionId string
param name string
param location string
param use32BitWorkerProcess bool
param ftpsState string
param storageAccountName string


param sku string
param skuCode string
param workerSize string
param workerSizeId string

param hostingPlanName string
param serverFarmResourceGroup string

var contentShare = 'fa-constructix-onlineservices-getsuppliersb0d1'

resource hostingPlan 'Microsoft.Web/serverfarms@2024-11-01' = {
  name: hostingPlanName
  location: location
  kind: ''
  tags: {
    Company: 'Constructix'
    Environment: 'dev'
    Project: 'Constructix Online Services'
  }
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: true
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
    asyncScalingEnabled: false
  }
  sku: {
    name: 'FC1'
    tier: 'FlexConsumption'
    size: 'FC1'
    family: 'FC'
    capacity: 0
  }
  dependsOn: []
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: storageAccountName
  location: location
  tags: {
    Company: 'Constructix'
    Environment: 'dev'
    Project: 'Constructix Online Services'
  }
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
  properties: {
    supportsHttpsTrafficOnly: true
    minimumTlsVersion: 'TLS1_2'
    defaultToOAuthAuthentication: true
  }
  dependsOn: [hostingPlan]
}

resource name_resource 'Microsoft.Web/sites@2023-12-01' = {
  name: name
  kind: 'functionapp,linux'
  location: location
  tags: {}
  properties: {
    name: name
    siteConfig: {
      appSettings: [
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }       
        {
          name: 'WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED'
          value: '1'
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};AccountKey=${listKeys(storageAccount.id,'2019-06-01').keys[0].value};EndpointSuffix=core.windows.net'
        }       
      ]
      cors: {
        allowedOrigins: [
          'https://portal.azure.com'
        ]
      }
      use32BitWorkerProcess                             : use32BitWorkerProcess
      ftpsState                                         : ftpsState
     
    }
    functionAppConfig: {
      runtime: { 
        name:'dotnet-isolated' 
      version: '10.0' 
    }
          scaleAndConcurrency: {
        instanceMemoryMB: 512  // Choose: 512, 2048, or 4096
        maximumInstanceCount: 40  // Optional: scale limit (min 40, max 1000)
        // For HTTP triggers, you can also specify:
        // httpPerInstanceConcurrency: 100
      }
      deployment: {
        storage: {
          type: 'blobcontainer'
          value: 'https://sadevaeconstructixs01.blob.core.windows.net/app-package-testdev'
          authentication: {
            type: 'storageaccountconnectionstring'
            storageAccountConnectionStringName: 'DEPLOYMENT_STORAGE_CONNECTION_STRING'
          }
        }
      }
    }
    clientAffinityEnabled: false
    virtualNetworkSubnetId: null
    publicNetworkAccess: 'Enabled'
    httpsOnly: true
    serverFarmId: '/subscriptions/${subscriptionId}/resourcegroups/${serverFarmResourceGroup}/providers/Microsoft.Web/serverfarms/${hostingPlanName}'
  }
  dependsOn: [
    hostingPlan, storageAccount
  ]
}

resource name_scm 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2022-09-01' = {
  parent: name_resource
  name: 'scm'
  properties: {
    allow: false
  }
}

resource name_ftp 'Microsoft.Web/sites/basicPublishingCredentialsPolicies@2022-09-01' = {
  parent: name_resource
  name: 'ftp'
  properties: {
    allow: false
  }
}
