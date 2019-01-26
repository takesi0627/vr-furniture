using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace hekira
{
    public class Text : UnityEngine.UI.Text
    {
        [SerializeField] TextIndex m_TextIndex;
        public TextIndex TextIndex
        {
            get { return m_TextIndex; }
            set {
                if (m_TextIndex != value) {
                    m_TextIndex = value;
                    text = TextUtility.GetText(m_TextIndex);
                }
            }
        }

        /// <summary>
        /// Override setter to perform special text "index:" for quick transform
        /// </summary>
        /// <value>The text.</value>
        public override string text
        {
            get
            {
                return base.text;
            }

            set
            {
                if ((value != null) && value.StartsWith("index:", StringComparison.Ordinal)) {
                    string eText = value.Substring("index:".Length);
                    try
                    {
                        TextIndex eTextIndex = (TextIndex)Enum.Parse(typeof(TextIndex), eText);
                        TextIndex = eTextIndex;
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(ex.Message);
                    }
                }
                else
                    base.text = value;
            }
        }

        public void OnLanguageChanged()
        {
            text = TextUtility.GetText(TextIndex);
        }

        protected override void Start()
        {
            base.Start();
            Configuration.ChangeLanguageEventHandler += OnLanguageChanged;
            text = TextUtility.GetText(TextIndex);
        }
    }
}