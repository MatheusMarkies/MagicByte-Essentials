using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics.Logic
{
    public class Arrays
    {
        public static List<T> ArrayToList<T>(T[] array)
        {
            List<T> list = new List<T>();
            foreach (T t in array)
                list.Add(t);
            return list;
        }
        public static T[] ListToArray<T>(List<T> list)
        {
            T[] t = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
                t[i] = list[i];
            return t;
        }
    }
}
