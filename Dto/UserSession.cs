namespace Pokladna.Dto
{
    public class UserSession
    {
        // private set zajistí, že to zvenčí po vytvoření taky nikdo nepřepíše
        public string FullName { get; private set; }
        public string Role { get; private set; }

        public UserSession(string fullName, string role)
        {
            FullName = fullName;
            Role = role;
        }
    }
}
