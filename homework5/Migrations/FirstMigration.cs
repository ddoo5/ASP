using FluentMigrator;

namespace MetricsAgent
{
    [Migration(1)]
    public class FirstMigration : Migration
	{
        private readonly ILogger<FirstMigration> _logger;

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");

            _logger.LogDebug(3, "All tables dropped");
        }

        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            Create.Table("rammetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            _logger.LogDebug(3, "All tables created");
        }
    }
}

