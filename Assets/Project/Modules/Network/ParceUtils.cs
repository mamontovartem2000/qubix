using UnityEngine;

namespace Project.Modules.Network
{
    public static class ParceUtils
    {
        public static T CreateFromJSON<T>(string jsonString)
        {
            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}
