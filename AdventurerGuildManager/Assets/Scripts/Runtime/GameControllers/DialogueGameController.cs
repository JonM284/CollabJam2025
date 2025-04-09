using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project.Scripts.Utils;

namespace Runtime.GameControllers
{
    public class DialogueGameController: GameControllerBase
    {
        #region Instance

        public static DialogueGameController Instance { get; private set; }

        #endregion

        #region Actions

        public static event Action<List<string>> requestNewSetOfSentences; 
        
        public static event Action<string> requestNewSentence; 

        #endregion
        
        #region GameControllerBase Inherited Methods

        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            base.Initialize();
        }

        #endregion

        #region Class Implementation

        public void DisplayNewSentences(List<string> _sentences)
        {
            requestNewSetOfSentences?.Invoke(_sentences);
        }

        public void DisplaySingleSentence(string _sentence)
        {
            requestNewSentence?.Invoke(_sentence);
        }

        #endregion
        
    }
}