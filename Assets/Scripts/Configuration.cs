using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hekira{
    public static class Configuration
    {
        public delegate void ChangeLanguageEvent();
        static public event ChangeLanguageEvent ChangeLanguageEventHandler;

        static Language m_Language = Language.JP;
        public static Language Language {
            get { return m_Language; }
        }

        public static void SetLanguage (Language eLan) {
            if (eLan != m_Language)
            {
                Debug.LogFormat("Set Language from {0} to {1}", m_Language.ToString() , eLan.ToString());
                m_Language = eLan;
                ChangeLanguageEventHandler();
            }
        }
    }
}
