using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using TestDemo.Authorization.Users;
using TestDemo.MultiTenancy;

namespace TestDemo.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}