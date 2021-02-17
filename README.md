# BASTA! Spring 2021

Source code samples for

[C# 9 - what's the cool stuff?](https://www.slideshare.net/christiannagel/c-9-whats-the-cool-stuff-basta-spring-2021)

[Azure App Configuration with .NET Applications](https://www.slideshare.net/christiannagel/azure-app-configuration-with-net-applications)


## C# 9 - What's the cool stuff?

Samples:

* Route to Code (API with top-level statements, )
* EF Core with records (see the Update method with the detach-requirement)
* Source generators with simple code generation, and creating and reading attributes and creating code with partial methods

## Azure App Configuration with .NET Applications

### 1 .NET Configuration

Just use the sample!

### 2 - Azure App Configuration

To use the sample:

1. create an Azure App Configuration (see the script in the setup directory)
2. copy the connection string from Azure App Configuration to the user secrets, key: `AzureAppConfigurationConnection`

## 3 - Identity

1. With Azure App Configuration, add a role to enable a user with read access
2. Configure this user with launchSettings.json

## 4- Refresh and Environments

Configure launchsettings.json, like 3.

## 5 - Feature Flags

Add the feature "FeatureX" to Azure App Configuration with a percentage-filter

## 6- Key Vault

1. Configure an Azure Key Vault
2. Allow the same user configured before to allow read access to the secrets
3. Configure the key Vault with Azure App Configuration

Enjoy!