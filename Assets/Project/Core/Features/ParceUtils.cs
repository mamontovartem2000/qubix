using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Core.Features
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

        public GameMapRemoteData(TextAsset sourceMap)
        {
            var omg = sourceMap.text.Split('\n');
            offset = omg[0].Length - 1;
            List<byte> sd = new List<byte>();

            foreach (var line in omg)
            {
                for (int i = 0; i < offset; i++)
                {
                    var num = Convert.ToByte(char.GetNumericValue(line[i]));
                    sd.Add(num);
                }
            }

            bytes = sd.ToArray();

            //int count = 0;
            //string df = string.Empty;

            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    count++;
            //    df += bytes[i].ToString();

            //    if (count == offset)
            //    {
            //        count = 0;
            //        df += "\n";
            //    }
            //}
            //Debug.Log(df);
        }
    }
}
