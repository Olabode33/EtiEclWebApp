using System.Collections.Generic;
using MvvmHelpers;
using TestDemo.Models.NavigationMenu;

namespace TestDemo.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}