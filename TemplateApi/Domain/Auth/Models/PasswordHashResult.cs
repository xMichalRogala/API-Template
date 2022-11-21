namespace TemplateApi.Domain.Auth.Models
{
    public class PasswordHashResult
    {
        public PasswordHashResult(string salt, string key, int iterations)
        {
            Salt = salt;
            Key = key;
            Iterations = iterations;
        }

        public string Salt { get; set; }
        public string Key { get; set; }
        public int Iterations { get; set; }
    }
}
