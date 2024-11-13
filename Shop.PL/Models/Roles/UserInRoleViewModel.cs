namespace Shop.PL.Models.Roles
{
    public class UserInRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
