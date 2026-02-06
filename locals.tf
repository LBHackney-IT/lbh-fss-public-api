locals {
  environment = terraform.workspace

  config = {
    development = {
      vpc_name                               = "apis-dev"
      vpc_env_tag                            = "Dev"
      db_identifier                          = "fss-public-dev-db"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 20
      storage_encrypted                      = false
      multi_az                               = false
      deletion_protection                    = false
      maintenance_window                     = "sun:10:00-sun:10:30"
      centralised_parameter_store_account_id = "115283375626"
    }
    staging = {
      vpc_name                               = "apis-stg"
      vpc_env_tag                            = "Staging"
      db_identifier                          = "fss-public-stg-db"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 20
      storage_encrypted                      = true
      multi_az                               = false
      deletion_protection                    = true
      maintenance_window                     = "sun:10:00-sun:10:30"
      centralised_parameter_store_account_id = "469511945406"
    }
    production = {
      vpc_name                               = "apis-prod"
      vpc_env_tag                            = "Production"
      db_identifier                          = "fss-public-prod-db"
      db_instance_class                      = "db.t3.micro"
      db_allocated_storage                   = 50
      storage_encrypted                      = true
      multi_az                               = true
      deletion_protection                    = true
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