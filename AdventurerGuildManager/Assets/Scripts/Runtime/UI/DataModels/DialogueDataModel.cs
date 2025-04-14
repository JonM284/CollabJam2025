using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Project.Scripts.Utils;
using Runtime.GameControllers;
using TMPro;
using UnityEngine;

namespace Runtime.UI.DataModels
{
    public class DialogueDataModel: MonoBehaviour
    {

        #region Actions

        public static event Action onLetterAdded;

        public static event Action onSentenceFinished;

        public static event Action onDialogueClosed;

        #endregion
        
        [SerializeField] private GameObject m_holder;
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private float m_textSpeed;

        private int m_index;
        private bool m_hasFinishedWriting, m_hasPressedToEnd;
        private List<string> m_currentDialogues = new List<string>();

        public void OnEnable()
        {
            DialogueGameController.requestNewSetOfSentences += DialogueGameControllerOnrequestNewSetOfSentences;
            DialogueGameController.requestNewSentence += DialogueGameControllerOnrequestNewSetOfSentences;
        }

        public void OnDisable()
        {
            DialogueGameController.requestNewSetOfSentences -= DialogueGameControllerOnrequestNewSetOfSentences;
            DialogueGameController.requestNewSentence -= DialogueGameControllerOnrequestNewSetOfSentences;
        }

        private void DialogueGameControllerOnrequestNewSetOfSentences(List<string> _newSentences)
        {
            if (_newSentences.IsNull())
            {
                CloseDialog();
                return;
            }
            
            m_text.text = string.Empty;
            m_holder.SetActive(true);
            m_currentDialogues.Clear();
            m_currentDialogues = _newSentences.ToList();
            m_index = 0;
            DrawSentence();
        }
        
        private void DialogueGameControllerOnrequestNewSetOfSentences(string _newSentence)
        {
            if (string.IsNullOrEmpty(_newSentence))
            {
                CloseDialog();
                return;
            }
            
            m_text.text = string.Empty;
            m_holder.SetActive(true);
            m_currentDialogues.Clear();
            m_currentDialogues.Add(_newSentence);
            m_index = 0;
            DrawSentence();
        }

        public void MoveNextSentence()
        {
            if (!m_hasFinishedWriting)
            {
                m_hasPressedToEnd = true;
                return;
            }

            m_hasPressedToEnd = false;
            m_index++;
            m_text.text = string.Empty;
            
            if (m_index < m_currentDialogues.Count)
            {
                DrawSentence();
            }
            else
            {
                CloseDialog();
            }
        }

        private void CloseDialog()
        {
            m_holder.SetActive(false);
            onDialogueClosed?.Invoke();
        }

        public async UniTask DrawSentence()
        {
            if (m_currentDialogues[m_index].IsNull())
            {
                onSentenceFinished?.Invoke();
                return;
            }
            
            m_hasFinishedWriting = false;
            
            foreach (char _character in m_currentDialogues[m_index].ToCharArray())
            {
                if (m_hasPressedToEnd)
                {
                    break;
                }
                
                onLetterAdded?.Invoke();
                m_text.text += _character;
                await UniTask.WaitForSeconds(m_textSpeed);
            }

            m_text.text = m_currentDialogues[m_index];
            m_hasFinishedWriting = true;
            onSentenceFinished?.Invoke();
        }


    }   
}