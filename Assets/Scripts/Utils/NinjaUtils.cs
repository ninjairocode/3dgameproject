using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class NinjaUtils
    {
        #if UNITY_EDITOR
        [UnityEditor.MenuItem("Ebac/Test")]
        public static void Test()
        {
            
            Debug.Log("Test");
        }
        
        [UnityEditor.MenuItem("Ebac/Test2 %g")]
        public static void Test2()
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            Selection.activeGameObject = cube;
            
            Debug.Log("Cubo Adicionado");
        }
        #endif

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandom<T>(this T[] array)
        {
            if(array.Length == 0)
                return default(T);
            
            return array[Random.Range(0, array.Length)];
        }

        public static T GetRandomButNotSame<T>(this List<T> list, T unique)
        {
            if (list.Count == 1)
                return unique;
            
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];   
        }
    
    }
}
