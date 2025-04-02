namespace Runtime.Characters
{
    public interface ICharacter
    {
        public string name { get; protected set; }

        public void BeginInteraction();
        public void Deny();
        public void Accept();
    }
}