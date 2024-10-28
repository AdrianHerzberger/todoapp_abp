using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoapp.Migrations
{
    /// <inheritdoc />
    public partial class Added_TodoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "TodoItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false), // UUID represented as a string
                Text = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false) // Adjust maxLength as needed
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TodoItems", x => x.Id);
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "TodoItems");

        }
    }
}
