using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTrip2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.InsertData(
           table: "Trips",
               columns: new[] { "Id", "StorageId", "DriverId", "TruckId", "TrailerId", "GateId", "TimeslotId", "CompanyId", "ArrivalTimePlan", "NowStatus", "Number", "PalletsCount" },
               values: new object[,]
               {
                    { new Guid("6a3733ab-63e3-4c5c-a2c9-9f44397d4393"), "36db5899-46b0-4905-83dd-594bb5de55e8", "abc952a2-ad52-4a74-912d-1c0acc468712", "f198c71a-bc25-49ac-b8ff-322b82bfe017", "b2bef280-b259-4bb8-abfb-ecdd8715a59e", "da712b07-61ce-4d76-84ba-1383786f7fd7", "b120c29d-3610-46d7-bee9-0d7e2836baf9", "c7b2ae0c-aef7-4ce4-87cb-642a2182b84b", new DateTime(2023,03,28,16,00,00,DateTimeKind.Utc), 0, "000001", "30"},
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
