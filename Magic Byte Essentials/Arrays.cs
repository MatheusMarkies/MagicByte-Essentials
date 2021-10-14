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
        public static List<Y> DictionaryToList<T, Y>(Dictionary<T, Y> dictionary)
        {
            List<Y> list = new List<Y>();
            foreach (Y y in dictionary.Values)
                list.Add(y);
            return list;
        }
        public static Dictionary<T, Y> ListToDictionary<T, Y>(T[] key,List<Y> list)
        {
            Dictionary<T, Y> dictionary = new Dictionary<T, Y>();
            for (int i = 0; i < list.Count; i++)
                if(i<key.Length)
                dictionary.Add(key[i], list[i]);

            return dictionary;
        }
        public static Dictionary<int, T> ListToDictionary<T>(List<T> list)
        {
            Dictionary<int, T> dictionary = new Dictionary<int, T>();
            for (int i =0;i<list.Count;i++)
                dictionary.Add(i, list[i]);
            return dictionary;
        }

        public static List<T> invertList<T>(List<T> list)
        {
            List<T> invertList = new List<T>();
      
            for(int i = list.Count-1; i > 0; i--)
            {
                invertList.Add(list[i]);
            }

            return invertList;
        }
        public static List<T> addList<T>(List<T> listA, List<T> listB)
        {
            foreach (T t in listB) {
                listA.Add(t);
            }
            return listA;
        }
    }
}
