using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecieveNewsLetters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1da2fa81-8f67-4492-91a0-6d511895fbcc"), "Germany" },
                    { new Guid("1e77f8b3-1c4d-4622-97a7-8c2b47ef2e26"), "Canada" },
                    { new Guid("6aab3d8a-31b4-4590-82f3-820812bded5f"), "Brazil" },
                    { new Guid("83a2c0a1-2761-4244-bf87-5ce4393c3ade"), "Angola" },
                    { new Guid("83ce1cec-78c3-4703-975a-cb1b9a1cc176"), "Argentina" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "Gender", "Name", "Password", "RecieveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("0db3e5f3-b610-4af5-b314-0fa9763e68fd"), "24 Ajebo street", null, new DateTime(1997, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "James.catch@gmail.com", "Male", "James", null, false },
                    { new Guid("2b5a85e3-de43-48a8-8fde-8b62b367c428"), "17 cyber street", null, new DateTime(2002, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack34@gmail.com", "Male", "Jack", null, false },
                    { new Guid("93f16a89-6ee0-482f-ba0a-8431f1a31de6"), "16 mavin street", null, new DateTime(1999, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucifer.dot@gmail.com", "Male", "Lucifer", null, true },
                    { new Guid("9c6958ef-b34a-41e6-8596-5122b27979a3"), "15 Elon street", null, new DateTime(2013, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davina.agile@gmail.com", "Female", "Davina", null, false },
                    { new Guid("f60277bc-d984-43b6-9953-63285d04f5b1"), "12 mario street", null, new DateTime(2003, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mario247@gmail.com", "Male", "Mario", null, true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
