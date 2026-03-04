variable "environment" {
  description = "The environment name (development, staging, production)"
  type        = string
}

locals {
  environment = var.environment

  config = {
    development = {
      vpc_name                               = "apis-dev"
      vpc_env_tag                            = "Dev"
      env_tag                                = "dev"
      db_identifier                          = "fss-public-dev-db"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 20
      storage_encrypted                      = true
      multi_az                               = false
      deletion_protection                    = false
      apply_immediately                      = true
      maintenance_window                     = "sun:10:00-sun:10:30"
      centralised_parameter_store_account_id = "115283375626"
      additional_tags = {
        BackupPolicy = "Dev"
      }
    }
    staging = {
      env_tag                                = "stg"
      vpc_name                               = "apis-stg"
      vpc_env_tag                            = "Stg"
      db_identifier                          = "fss-public-encrypted"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 20
      storage_encrypted                      = true
      multi_az                               = false
      deletion_protection                    = true
      apply_immediately                      = true
      maintenance_window                     = "sun:10:00-sun:10:30"
      centralised_parameter_store_account_id = "469511945406"
      additional_tags = {
        BackupPolicy = "Stg"
      }
    }
    production = {
      env_tag                                = "prod"
      vpc_name                               = "apis-prod"
      vpc_env_tag                            = "Prod"
      db_identifier                          = "fss-public-prod-db"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 50
      storage_encrypted                      = true
      multi_az                               = true
      deletion_protection                    = true
      apply_immediately                      = true
      maintenance_window                     = "sun:10:00-sun:10:30"
      centralised_parameter_store_account_id = "918025132036"
      additional_tags = {
        BackupPolicy = "Prod"
      }
    }
  }

  current_config              = local.config[local.environment]
  application_name            = "fss public api"
  parameter_store             = "arn:aws:ssm:${data.aws_region.current.name}:${data.aws_caller_identity.current.account_id}:parameter"
  centralised_parameter_store = "arn:aws:ssm:${data.aws_region.current.name}:${local.current_config.centralised_parameter_store_account_id}:parameter"
}
