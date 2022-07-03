namespace BookStore.WebApplication.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string returnURL { get; set; }
    }
}
