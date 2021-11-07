using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputedColumns.POC.EntityFramework.Migrations
{
    public partial class Add_Full_Text_Search_Index_On_Animals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;",
                suppressTransaction: true);

            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON Animals(ComputedProperty) KEY INDEX PK_Animals;",
                suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
