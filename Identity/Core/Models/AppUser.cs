﻿namespace Identity.Core.Models;

using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public DateTime CreatedDate { get; set; }
}