﻿namespace Parking.Data.DTO
{
    public class RegisterRequestDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ParkName { get; set; }

    }
}
