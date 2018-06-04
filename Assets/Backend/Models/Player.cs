﻿using Unirea_UI.Models;

namespace Assets.Backend.Models
{
    public class Player
    {
        public string AuthenticationToken { get; set; }
        public int id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Player(int id, string authenticationToken, string username, string email, string password)
        {
            AuthenticationToken = authenticationToken;
            Username = username;
            Email = email;
            Password = password;
        }

        public Player(int id, string authenticationToken, string username, string password)
        {
            AuthenticationToken = authenticationToken;
            Username = username;
            Email = null;
            Password = password;
        }
    }
}
