data "aws_caller_identity" "current" {}
data "aws_region" "current" {}

data "aws_vpc" "vpc" {
  tags = {
    Name = local.current_config.vpc_name
  }
}

data "aws_subnets" "private_subnets" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.vpc.id]
  }

  filter {
    name   = "tag:Environment"
    values = [local.current_config.vpc_env_tag]
  }
}

# Sensative parameters such as database credentials are stored in the parameter store of each environment's account. In future this will be moved to a centralised secret manager to further reduce risk of error and duplication when updating values.

data "aws_ssm_parameter" "postgres_password" {
  name = "/fss-public-api/${local.environment}/postgres-password"
}

data "aws_ssm_parameter" "postgres_username" {
  name = "/fss-public-api/${local.environment}/postgres-username"
}

# Low value parameters are stored in a centralised parameter store to reduce risk of error when updating values and ensure swift disaster recovery. These parameters are accessed using the centralised_parameter_store variable in locals.tf which uses the account ID from the current environment's config to determine which parameter store to pull from.

data "aws_ssm_parameter" "postgres_port" {
  name = "${local.centralised_parameter_store}/${local.environment}apis/fss-public-api-${local.environment}/fss-public-api/postgres-port"
}

data "aws_ssm_parameter" "postgres_database" {
  name = "${local.centralised_parameter_store}/${local.environment}apis/fss-public-api-${local.environment}/fss-public-api/postgres-database"
}