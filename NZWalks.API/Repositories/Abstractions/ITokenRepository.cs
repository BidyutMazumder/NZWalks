﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Abstractions
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
