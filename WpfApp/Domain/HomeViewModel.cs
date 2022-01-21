using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace WpfApp.Domain
{
    public class HomeViewModel : ViewModelBase
    {
        public string WindowsIdentityUserName { get; }
        public string UserPrincipalDisplayName { get; }
        public string UserFolder { get; }

        public HomeViewModel()
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            WindowsIdentityUserName = windowsIdentity.Name;
            var principal = UserPrincipal.Current;
            UserPrincipalDisplayName = principal.DisplayName;
            UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}
