﻿namespace Dashboard.WebAPI.DTO
{
    public class UserStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;       
        public string CompanyName { get; set; } = string.Empty; 
    }
}
