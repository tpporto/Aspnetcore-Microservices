using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "serial", nullable: false),
                    UserName = table.Column<string>(type: "character varying(255)", nullable: true),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", nullable: true),
                    LastName = table.Column<string>(type: "character varying(255)", nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(255)", nullable: true),
                    AddressLine = table.Column<string>(type: "character varying(255)", nullable: true),
                    Country = table.Column<string>(type: "character varying(255)", nullable: true),
                    State = table.Column<string>(type: "character varying(255)", nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(255)", nullable: true),
                    CardName = table.Column<string>(type: "character varying(255)", nullable: true),
                    CardNumber = table.Column<string>(type: "character varying(255)", nullable: true),
                    Expiration = table.Column<string>(type: "character varying(255)", nullable: true),
                    CVV = table.Column<string>(type: "character varying(255)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(255)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(255)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}