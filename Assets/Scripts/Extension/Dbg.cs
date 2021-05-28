using System;
using UnityEngine;


namespace Extension
{
    public static class Dbg
    {
        public static void Log(string message)
        {
            Debug.Log($"{message}");
        }

        public static void Log(Enum @enum)
        {
            Debug.Log($"Console.{@enum.GetType()}:{@enum}");
        }

        public static void Error(string message)
        {
            Debug.LogError($"{message}");
        }
    }
}