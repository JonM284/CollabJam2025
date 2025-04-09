namespace Runtime.Characters
{
    public interface ICharacter
    {
        public void BeginInteraction();
        public void OnDeny();
        public void OnAccept();
    }
}