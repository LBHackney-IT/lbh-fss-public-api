module "postgres_db" {
  source = "github.com/LBHackney-IT/aws-hackney-common-terraform.git//modules/database/postgres"

  environment_name        = local.environment
  vpc_id                  = data.aws_vpc.vpc.id
  db_engine               = "postgres"
  db_engine_version       = "16.11"
  db_parameter_group_name = "postgres-16"
  db_identifier           = local.current_config.db_identifier
  db_instance_class       = local.current_config.db_instance_class
  db_name                 = data.aws_ssm_parameter.postgres_database.value
  db_port                 = data.aws_ssm_parameter.postgres_port.value
  db_username             = data.aws_ssm_parameter.postgres_username.value
  db_password             = data.aws_ssm_parameter.postgres_password.value
  subnet_ids              = data.aws_subnets.private_subnets.ids
  db_allocated_storage    = local.current_config.db_allocated_storage
  maintenance_window      = local.current_config.maintenance_window
  storage_encrypted       = local.current_config.storage_encrypted
  multi_az                = local.current_config.multi_az
  deletion_protection     = local.current_config.deletion_protection
  apply_immediately       = local.current_config.apply_immediately
  publicly_accessible     = false
  project_name            = local.application_name
  copy_tags_to_snapshot   = true
  additional_tags         = lookup(local.current_config, "additional_tags", {})
}
