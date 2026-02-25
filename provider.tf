terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.67"
    }
  }
  backend "s3" {
    encrypt = true
    region  = "eu-west-2"
    key     = "services/fss-public-api/terraform.tfstate"
  }
}

provider "aws" {
  region = "eu-west-2"
  default_tags {
    tags = {
      Application = local.application_name
      TeamEmail   = "developmentteam@hackney.gov.uk"
      Environment = local.current_config.env_tag
    }
  }
}