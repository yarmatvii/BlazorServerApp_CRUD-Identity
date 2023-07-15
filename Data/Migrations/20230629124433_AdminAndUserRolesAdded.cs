using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminAndUserRolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Admin', 'ADMIN')");
            _ = migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('2', 'User', 'USER')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '1'");
            _ = migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '2'");
        }
    }
}
