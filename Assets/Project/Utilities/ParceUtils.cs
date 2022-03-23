using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utilities
{
    public class ParceUtils
    {
        public static T CreateFromJSON<T>(string jsonString)
        {
            return JsonUtility.FromJson<T>(jsonString);
        }
    }

    [Serializable]
    public class UniversalData<T>
    {
        public T data;
    }

    [Serializable]
    public class GameMapRemoteData //TODO: Сопоставить названия полей с серверными
    {
        public byte[] bytes;
        public int offset;

        public GameMapRemoteData(TextAsset _sourceMap)
        {
            var omg = _sourceMap.text.Split('\n');
            offset = omg.Length;
            List<byte> sd = new List<byte>();

            foreach (var line in omg)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    sd.Add(Convert.ToByte(line[i]));
                }
            }

            bytes = sd.ToArray();
        }
    }
}
