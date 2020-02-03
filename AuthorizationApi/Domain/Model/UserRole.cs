namespace AuthorizationApi.Domain.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public UserRole(string role)
        {
            Role = role;
        }

    }
}
