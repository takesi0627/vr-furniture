using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum Language
{
    JP,
    CN,
    EN,
    KR,
}

namespace hekira
{
    [ExecuteInEditMode]
    public class TextUtility
    {
        static bool initialized = false;
        static string DATA_PATH = "language";
        static Dictionary<TextIndex, Dictionary<Language, string>> m_TextMap = new Dictionary<TextIndex, Dictionary<Language, string>>();

#if UNITY_EDITOR
        [MenuItem("Assets/TextUtility/Reload TextIndex")]
#endif
        public static void LoadData () {
            // clear data
            m_TextMap.Clear();

            List<string[]> data = CSVReader.LoadCSV(DATA_PATH);

            // skip first line
            for (int i = 1; i < data.Count; i++) {
                string[] line = data[i];

                // parse index
                long nIndex = 0;
                if (Int64.TryParse(line[0], out nIndex))
                {
                    TextIndex eIndex = (TextIndex)nIndex;

                    // check not duplicate
                    if (!m_TextMap.ContainsKey(eIndex))
                    {
                        // parse translation
                        Dictionary<Language, string> kTranslate = new Dictionary<Language, string>();
                        for (int j = 1; j < line.Length; j++)
                        {
                            kTranslate.Add((Language)(j - 1), line[j]);
                        }

                        // add pair
                        m_TextMap.Add(eIndex, kTranslate);
                    }
                }
            }
        }

        /// <summary>
        /// Search text and return it by language and text index.
        /// </summary>
        /// <returns>The text. </returns>
        /// <param name="eTextIndex">E text index.</param>
        public static string GetText(TextIndex eTextIndex)
        {
            if (!initialized) {
                LoadData();
                initialized = true;
            }

            Language currentLanguage = Configuration.Language;
    
        // check text index valid
            if (m_TextMap.ContainsKey(eTextIndex))
            {
                // check language valid
                if (m_TextMap[eTextIndex].ContainsKey(currentLanguage))
                {
                    return m_TextMap[eTextIndex][currentLanguage];
                }
            }

            // if nothing hit, return empty string
            Debug.LogErrorFormat("Cannot find {0:####} in TextIndex", (int)eTextIndex);
            return "";
        }
    }
}