﻿using System.Dynamic;
using System.Security.Cryptography;

namespace TaskManagerDemo.Domain.Users.Aggregates;

public record UserId(Guid Value)
{
    public static UserId From(string id) => new(Guid.Parse(id));
};