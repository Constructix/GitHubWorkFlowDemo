az deployment group validate `
    --resource-group rg-constructix-dev-ae-01 `
    --template-file .\CreateFunction.bicep `
    --parameters .\function-dev-parameters.json