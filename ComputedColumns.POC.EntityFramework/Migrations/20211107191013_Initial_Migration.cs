using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputedColumns.POC.EntityFramework.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Breed = table.Column<string>(nullable: true),
                    RegistrationCode = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ComputedProperty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ComputedProperty",
                table: "Animals",
                column: "ComputedProperty")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
