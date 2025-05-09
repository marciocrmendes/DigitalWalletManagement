﻿namespace DigitalWalletManagement.Features.Users.CreateUser
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
