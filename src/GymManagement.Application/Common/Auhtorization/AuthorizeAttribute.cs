﻿namespace GymManagement.Application.Common.Auhtorization;

[AttributeUsage(AttributeTargets.Class , AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    public string? Permissions { get; set; }
    public string? Roles { get; set; }
}
