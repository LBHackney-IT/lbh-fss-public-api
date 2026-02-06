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
    key     = "services/fss-public-api/state"
  }
}

provider "aws" {
  region = "eu-west-2"
}