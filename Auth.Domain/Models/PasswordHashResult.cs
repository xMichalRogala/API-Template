namespace Auth.Domain.Models
{
    public class PasswordHashResult
    {
        public PasswordHashResult(string salt, byte[] key, int iterations)
        {
            Salt = salt;
            Key = key;
            Iterations = iterations;
        }

        public string Salt { get; set; }
        public byte[] Key { get; set; }
        public int Iterations { get; set; }
    }
}
