using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChurchCashFlow.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    District = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    Street = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Additional = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Number = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Church",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Church", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Church_Address",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "VARCHAR(6)", maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ChurchId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_User_Church",
                        column: x => x.ChurchId,
                        principalTable: "Church",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "Additional", "City", "Country", "District", "Number", "State", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "", "São Lourenço", "Brasil", "Centro", 780, "Minas Gerais", "Rua Dr Ribeiro da Luz", "37470-000" },
                    { 2, "Prédio 1", "Itaguai", "Brasil", "Mesquita", 258, "Rio de Janeiro", "Avenida André Chaves", "13710-000" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "LOCAL" },
                    { 2, "MINISTERIO" }
                });

            migrationBuilder.InsertData(
                table: "Church",
                columns: new[] { "Id", "AddressId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "CEO São Lourenço" },
                    { 2, 2, "CEP Cristina" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ChurchId", "Code", "Name", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, "7184CE", "Rodolfo de Jesus Silva", "10000.khFQMl1UKUKPQ0zHz7jERQ==.szHjjalK5LaSKcZh2RxbDLOFYMMED1jGwP9KzXS4GLw=", 1 },
                    { 2, 2, "C1A124", "Kelly Cristina Martins", "10000.5IT1HxPIczMvTl6OtRDWXw==.H2uYi58YltweftQ0U2RCSiwaaaigYNBpahdDbJSJdLQ=", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Church_AddressId",
                table: "Church",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ChurchId",
                table: "User",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Church");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
