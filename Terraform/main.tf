#############################################################################
# TERRAFORM CONFIG
#############################################################################

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.1.0"
    }
  }
}

#############################################################################
# VARIABLES
#############################################################################

variable "location" {
  type    = string
  default = "northeurope"
}

variable "environment" {
  type    = string
  default = "dev"
}

locals {
  tags = {
    environment = var.environment
    application = "tf-test"
  }
}
#############################################################################
# PROVIDERS
#############################################################################

provider "azurerm" {
  features {}
}

#############################################################################
# RESOURCES
#############################################################################

resource "azurerm_resource_group" "tf_group" {
  name     = "rg-tf-${var.environment}"
  location = var.location
}

resource "azurerm_storage_account" "tf_storage" {
  name = "tftfstorageaccount${var.environment}"
  location = azurerm_resource_group.tf_group.location
  resource_group_name = azurerm_resource_group.tf_group.name
  account_tier = "Standard"
  account_replication_type = "LRS"

  tags = {
    environment = var.environment
  }
}

resource "azurerm_app_service_plan" "tf_app_service_plan" {
  name                = "tf-app-service-plan"
  location            = azurerm_resource_group.tf_group.location
  resource_group_name = azurerm_resource_group.tf_group.name
  kind = "linux"
  reserved = true

  sku {
    tier = "Basic"
    size = "B1"
    capacity = 1
  }
}

resource "azurerm_app_service" "app-service" {
  name                = "tf-app-service"
  location            = azurerm_resource_group.tf_group.location
  resource_group_name = azurerm_resource_group.tf_group.name
  app_service_plan_id = azurerm_app_service_plan.tf_app_service_plan.id

  site_config {
    linux_fx_version = "DOTNETCORE|3.1"
    scm_type         = "LocalGit"
  }

  app_settings = {
    "TableConnectionString" = azurerm_storage_account.tf_storage.primary_connection_string
  }
}

#############################################################################
# OUTPUTS
#############################################################################

output "publishing" {
  value = azurerm_app_service.app-service.site_credential
}