namespace Pokladna
{
    public class GiftCard
    {
        // Vlastnosti mají private set, takže jsou po vytvoření neměnné (immutable)
        public string Code { get; private set; }
        public string Holder { get; private set; }
        public int Balance { get; private set; }

        // Konstruktor, přes který data bezpečně naplníme v databázové vrstvě
        public GiftCard(string code, string holder, int balance)
        {
            Code = code;
            Holder = holder;
            Balance = balance;
        }
    }
}