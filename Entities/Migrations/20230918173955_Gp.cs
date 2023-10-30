using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class Gp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string hld = @"
            CREATE PROCEDURE [dbo].[Second_Get]
             AS BEGIN
                SELECT * FROM [dbo].[People]
            END
              ";
            migrationBuilder.Sql(hld);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string hld = @"
            DROP PROCEDURE [dbo].[Second_Get]
             AS BEGIN
                SELECT * FROM [dbo].[People]
            END
              ";
            migrationBuilder.Sql(hld);
        }
    }
}
