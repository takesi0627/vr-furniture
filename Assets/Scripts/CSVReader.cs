using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace hekira {
    public static class CSVReader {

        public static List<string[]> LoadCSV (string path) {
            List<string[]> result = new List<string[]>();
            TextAsset csvFile = Resources.Load<TextAsset>(path);

            StringReader stringReader = new StringReader(csvFile.text);

            while (stringReader.Peek() != -1) {
                string line = stringReader.ReadLine();
                result.Add(line.Split(','));
            }

            return result;
        }
    }
}
