namespace Runtime.Characters
{
    public interface ICharacter
    {
        public void BeginInteraction();
        public void FinishInteraction(bool _wasAccepted);
    }
}