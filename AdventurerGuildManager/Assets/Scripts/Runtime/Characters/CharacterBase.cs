using UnityEngine;

namespace Runtime.Characters
{
    public class CharacterBase: MonoBehaviour, ICharacter
    {
        protected string _name;

        string ICharacter.name
        {
            get => _name;
            set => _name = value;
        }

        public void BeginInteraction()
        {
            
        }

        public void Deny()
        {
            
        }

        public void Accept()
        {
            
        }
        
    }
}