﻿namespace Domain.Services.Files
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid DriverId { get; set; }
        public byte[] Data { get; set; }
    }
}