module "postgres_db" {
  source = "github.com/LBHackney-IT/aws-hackney-common-terraform.git//modules/database/postgres"

  environment_name        = local.environment
  vpc_id                  = data.aws_vpc.vpc.id
  db_engine               = "postgres"
  db_engine_version       = "16.13"
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
  publicly_accessible     = false
  project_name            = local.application_name
  copy_tags_to_snapshot   = true
  additional_tags         = lookup(local.current_config, "additional_tags", {})
}

resource "aws_security_group_rule" "postgres_ingress_from_lambda" {
  type                     = "ingress"
  description              = "Allow public API Lambda to access PostgreSQL"
  from_port                = data.aws_ssm_parameter.postgres_port.value
  to_port                  = data.aws_ssm_parameter.postgres_port.value
  protocol                 = "tcp"
  security_group_id        = module.postgres_db.db_security_group_id
  source_security_group_id = local.current_config.lambda_security_group_id
}

resource "aws_security_group_rule" "postgres_ingress_from_jump_box" {
  for_each = toset(data.aws_instance.jump_box.vpc_security_group_ids)

  type                     = "ingress"
  description              = "Allow SSM jump box to access PostgreSQL for migrations"
  from_port                = data.aws_ssm_parameter.postgres_port.value
  to_port                  = data.aws_ssm_parameter.postgres_port.value
  protocol                 = "tcp"
  security_group_id        = module.postgres_db.db_security_group_id
  source_security_group_id = each.value
}
