namespace Demo.PL.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>(); //relationship between user and roles is many to many
    }
}
