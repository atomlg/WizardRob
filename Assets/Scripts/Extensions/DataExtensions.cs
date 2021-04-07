using UnityEngine;

namespace Extensions
{
    public static class DataExtensions
    {
        public static string ToJson(this object self) => 
            JsonUtility.ToJson(self);
        
        public static T FromJson<T>(this string json) => 
            JsonUtility.FromJson<T>(json);

    }
}