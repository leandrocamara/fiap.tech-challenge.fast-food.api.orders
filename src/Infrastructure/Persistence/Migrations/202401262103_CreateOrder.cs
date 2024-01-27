using FluentMigrator;

namespace Infrastructure.Persistence.Migrations;

[Migration(202401262103)]
public class CreateOrder : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript($"{typeof(Initial).Namespace}.Scripts.{nameof(Initial)}.sql");
    }

    public override void Down()
    {
    }
}