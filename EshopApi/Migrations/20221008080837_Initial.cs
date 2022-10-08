using Microsoft.EntityFrameworkCore.Migrations;

namespace EshopApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "City", "Email", "FirstName", "LastName", "Phone", "State", "ZipCode" },
                values: new object[] { 1, null, null, "Ariyanbakhtari@gmail.com", "Ariyan", "Bakhtiari", null, "tehran", null });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "City", "Email", "FirstName", "LastName", "Phone", "State", "ZipCode" },
                values: new object[] { 2, null, null, "Anitabakhtari@gmail.com", "Anita", "Bakhtiari", null, "tehran", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2);
        }
    }
}
