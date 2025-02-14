using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace LaniakeaCode.Utilities
{
    public static class GenericHelper
    {
        /// <summary>
        /// Finds all objects of type T from the Resources folder.
        /// </summary>
        /// <typeparam name="T">The type of objects to find.</typeparam>
        /// <returns>A list of objects of type T found in the Resources folder.</returns>
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

        /// <summary>
        /// Finds all GraphTree assets in the project.
        /// </summary>
        /// <returns>A list of GraphTree assets found in the project.</returns>
        public static List<GraphSystem.GraphTree> FindAllGraphs()
        {
            string[] guids = AssetDatabase.FindAssets("t: GraphTree");
            GraphSystem.GraphTree[] graphTrees = new GraphSystem.GraphTree[guids.Length];

            for(int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                graphTrees[i] = AssetDatabase.LoadAssetAtPath<GraphSystem.GraphTree>(path);
            }
            return graphTrees.ToList();
        }
    }
}