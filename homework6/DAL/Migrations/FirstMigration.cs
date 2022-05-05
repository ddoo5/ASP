﻿using FluentMigrator;

namespace MManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
	{
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
            Delete.Table("Agents");
        }


        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt64();

            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt64();

            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt64();

            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt64();

            Create.Table("rammetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt64();

            Create.Table("Agents")
                .WithColumn("AgentId").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentUrl").AsString();
        }
    }
}
