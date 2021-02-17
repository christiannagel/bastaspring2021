#!/bin/bash

# set resoure group, location, and Azure App Configuration names
rg=rg-bastaspring2021
loc=westeurope
appconfig=bastacconfig2021

# Variables for Azure App Configuration
key1=AppConfigurationSolutionSample:MySettingsCategory:Setting1
val1="configuration value for key 1"
devval1="development value for key 1"
stagingval1="staging value for key 1"
prodval1="production value for key 1"
backgroundColorkey=AppConfigurationSolutionSample:MySettingsCategory:BackgroundColor
foregroundColorkey=AppConfigurationSolutionSample:MySettingsCategory:ForegroundColor
backgroundColor="lightgreen"
foregroundColor="black"

sentinelKey=AppConfigurationSolutionSample:MySettingsCategory:Sentinel
sentinelValue=1

# Create the resoures
az group create --location $loc --name $rg
az appconfig create --location $loc --name $appconfig --resource-group $rg 

# Set the configuration values
az appconfig kv set -n $appconfig --key $key1 --value "$val1" --yes
az appconfig kv set -n $appconfig --key $key1 --label Development --value "$devval1" --yes
az appconfig kv set -n $appconfig --key $key1 --label Staging --value "$stagingval1" --yes
az appconfig kv set -n $appconfig --key $key1 --label Production --value "$prodval1" --yes
az appconfig kv set -n $appconfig --key $backgroundColorkey --value "$backgroundColor" --yes
az appconfig kv set -n $appconfig --key $foregroundColorkey --value "$foregroundColor" --yes

az appconfig kv set -n $appconfig --key $sentinelKey --value $sentinelValue --yes