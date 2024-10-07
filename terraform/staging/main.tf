# INSTRUCTIONS:
# 1) ENSURE YOU POPULATE THE LOCALS
# 2) ENSURE YOU REPLACE ALL INPUT PARAMETERS, THAT CURRENTLY STATE 'ENTER VALUE', WITH VALID VALUES
# 3) YOUR CODE WOULD NOT COMPILE IF STEP NUMBER 2 IS NOT PERFORMED!
# 4) ENSURE YOU CREATE A BUCKET FOR YOUR STATE FILE AND YOU ADD THE NAME BELOW - MAINTAINING THE STATE OF THE INFRASTRUCTURE YOU CREATE IS ESSENTIAL - FOR APIS, THE BUCKETS ALREADY EXIST
# 5) THE VALUES OF THE COMMON COMPONENTS THAT YOU WILL NEED ARE PROVIDED IN THE COMMENTS
# 6) IF ADDITIONAL RESOURCES ARE REQUIRED BY YOUR API, ADD THEM TO THIS FILE
# 7) ENSURE THIS FILE IS PLACED WITHIN A 'terraform' FOLDER LOCATED AT THE ROOT PROJECT DIRECTORY

provider "aws" {
  region  = "eu-west-2"
  version = "~> 3.0"
}
data "aws_caller_identity" "current" {}
data "aws_region" "current" {}
locals {
  application_name = "fss public api"
  parameter_store = "arn:aws:ssm:${data.aws_region.current.name}:${data.aws_caller_identity.current.account_id}:parameter"
}

terraform {
  backend "s3" {
    bucket  = "terraform-state-staging-apis"
    encrypt = true
    region  = "eu-west-2"
    key     = "services/fss-public-api/state"
  }
}

/*    POSTGRES SET UP    */
data "aws_vpc" "staging_vpc" {
  tags = {
    Name = "apis-stg"
  }
}
data "aws_subnet_ids" "staging_private_subnets" {
  vpc_id = data.aws_vpc.staging_vpc.id
  filter {
    name   = "tag:Type"
    values = ["private"]
  }
}

data "aws_ssm_parameter" "fss_public_postgres_db_password" {
  name = "/fss-public-api/staging/postgres-password"
}

data "aws_ssm_parameter" "fss_public_postgres_username" {
  name = "/fss-public-api/staging/postgres-username"
}

data "aws_ssm_parameter" "fss_public_postgres_port" {
  name = "/fss-public-api/staging/postgres-port"
}

data "aws_ssm_parameter" "fss_public_postgres_database" {
  name = "/fss-public-api/staging/postgres-database"
}

module "postgres_db_staging" {
  source = "github.com/LBHackney-IT/aws-hackney-common-terraform.git//modules/database/postgres"
  environment_name = "staging"
  vpc_id = data.aws_vpc.staging_vpc.id
  db_engine = "postgres"
  db_engine_version = "11.8"
  db_identifier = "fss-public-staging"
  db_instance_class = "db.t2.micro"
  db_name = data.aws_ssm_parameter.fss_public_postgres_database.value
  db_port  = data.aws_ssm_parameter.fss_public_postgres_port.value
  db_username = data.aws_ssm_parameter.fss_public_postgres_username.value
  db_password = data.aws_ssm_parameter.fss_public_postgres_db_password.value
  subnet_ids = data.aws_subnet_ids.staging_private_subnets.ids
  db_allocated_storage = 20
  maintenance_window ="sun:10:00-sun:10:30"
  storage_encrypted = false
  multi_az = false //only true if production deployment
  publicly_accessible = false
  project_name = "fss public api"
}

import {
  to = module.postgres_db_staging_encrypted.aws_db_instance.lbh-db
  id = "fss-public-staging-db-staging-encrypted"
}

import {
  to = module.postgres_db_staging_encrypted.module.db_security_group.aws_security_group.lbh_db_traffic
  id = "sg-04c73000bf97eae7e"
}

import {
  to = module.postgres_db_staging_encrypted.aws_db_subnet_group.db_subnets
  id = "fsspublicstaging-db-subnet-staging"
}

module "postgres_db_staging_encrypted" {
  source                  = "github.com/LBHackney-IT/aws-hackney-common-terraform.git//modules/database/postgres"
  environment_name        = "staging"
  vpc_id                  = data.aws_vpc.staging_vpc.id
  db_engine               = "postgres"
  db_engine_version       = "16.3"
  db_parameter_group_name = "postgres-16"
  db_identifier           = "fss-public-encrypted"
  db_instance_class       = "db.t3.micro"
  db_name                 = data.aws_ssm_parameter.fss_public_postgres_database.value
  db_port                 = data.aws_ssm_parameter.fss_public_postgres_port.value
  db_username             = data.aws_ssm_parameter.fss_public_postgres_username.value
  db_password             = data.aws_ssm_parameter.fss_public_postgres_db_password.value
  subnet_ids              = data.aws_subnet_ids.staging_private_subnets.ids
  db_allocated_storage    = 20
  maintenance_window      = "sun:10:00-sun:10:30"
  multi_az                = false
  publicly_accessible     = false
  storage_encrypted       = true
  project_name            = "fss public api"
}