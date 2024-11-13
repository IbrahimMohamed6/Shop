namespace Shop.PL.Models.Roles
{
    public class RoleViewModel
    {

        public string RoleId { get; set; }
        public string Id { get; set; }
        public string RoleName { get; set; }
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
