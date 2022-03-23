using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace LaniakeaCode.Utilities
{
    public static class GenericHelper
    {
        public static List<T> FindAllObjectsFromResources<T>()
        {
            List<T> tmp = new List<T>();
            string ResourcesPath = Application.dataPath + "/Resources";
            string[] directories = Directory.GetDirectories(ResourcesPath, "*", SearchOption.AllDirectories);

            foreach(string dir in directories)
            {
                string dirPath = dir.Substring(ResourcesPath.Length + 1);
                T[] result = Resources.LoadAll(dirPath, typeof(T)).Cast<T>().ToArray();

                foreach(var item in result)
                {
                    if (!tmp.Contains(item))
                    {
                        tmp.Add(item);
                    }
                }
            }
            return tmp;
        }
    }
}