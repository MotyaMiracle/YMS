﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.Design;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntityToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "Inn" },
                values: new object[,]
                {
                    { new Guid("c7b2ae0c-aef7-4ce4-87cb-642a2182b84b"), "РосРейс", "5009900157"},
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Name", "Surname", "Patronymic", "Passport", "DateOfIssuePassport", "ExpirationDatePassport", "DriveLicense", "DateOfIssueDriveLicense", "ExpirationDriveLicense", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("abc952a2-ad52-4a74-912d-1c0acc468712"), "Иван", "Иванов", "Иванович", "4834357961", new DateOnly(2015,4,30), new DateOnly(2050,4,30), "9925123456", new DateOnly(2019,12,15), new DateOnly(2029,12,15), "89998887766"},
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Name", "Address", "Latitude", "Longitude", "OpeningHour", "ClosingHour", "DayOfWeeks" },
                values: new object[,]
                {
                    { new Guid("36db5899-46b0-4905-83dd-594bb5de55e8"), "СкладРос", "Россия, г. Костромаа, Рабочая ул., д.14", 26.843512, -21.749485, "00:00:00", "23:59:59", new[] {1,2,3,4,5,6 } },
                });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Name", "Height", "PalletHandlingTime", "Status", "OpeningHour", "ClosingHour", "StorageId" },
                values: new object[,]
                {
                    { new Guid("da712b07-61ce-4d76-84ba-1383786f7fd7"), "1", "250", "5", 0, "00:00", "23:59", "36db5899-46b0-4905-83dd-594bb5de55e8" },
                });

            migrationBuilder.InsertData(
                table: "Trailers",
                columns: new[] { "Id", "Number", "Description", "CompanyId", "CargoCapacity" },
                values: new object[,]
                {
                    { new Guid("b2bef280-b259-4bb8-abfb-ecdd8715a59e"), "7777xx", "Тент", "c7b2ae0c-aef7-4ce4-87cb-642a2182b84b", "50"},
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "Brand", "Number", "CompanyId", "Description" },
                values: new object[,]
                {
                    { new Guid("f198c71a-bc25-49ac-b8ff-322b82bfe017"), "Volvo", "x777xx", "c7b2ae0c-aef7-4ce4-87cb-642a2182b84b", ""},
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "Id", "Date", "From", "To", "TripId", "Status" },
                values: new object[,]
                {
                    { new Guid("b120c29d-3610-46d7-bee9-0d7e2836baf9"), new DateTime(2023,03,28,00,00,00,DateTimeKind.Utc), "16:00", "18:30", new Guid("6a3733ab-63e3-4c5c-a2c9-9f44397d4393"),0 },
                });



            migrationBuilder.InsertData(
                table: "HistoryEntries",
                columns: new[] { "Id", "Text", "UserId", "Date", "EntityId" },
                values: new object[,]
                {
                    { new Guid("a65e0939-4189-4b47-b3e3-ba16535443f4"), "Создал рейс", "ee976536-0900-4a50-a530-ff76ffd77302", new DateTime(2023,03,28,00,00,00,DateTimeKind.Utc), "6a3733ab-63e3-4c5c-a2c9-9f44397d4393" },
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
