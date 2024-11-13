namespace Shop.PL.Models.Users
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Fname { get; set; }  =null!;
        public string LName { get; set; }  =null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Role { get; set; }
        public string PhoneNumber { get; set; } = null!;

    }
}
