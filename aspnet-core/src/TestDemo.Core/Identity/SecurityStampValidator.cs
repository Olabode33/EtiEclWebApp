﻿using Abp.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Users;
using TestDemo.MultiTenancy;

namespace TestDemo.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock)
            : base(options, signInManager, systemClock)
        {
        }
    }
}