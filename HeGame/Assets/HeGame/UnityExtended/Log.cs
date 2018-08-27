using System;

namespace HeGame
{
    public static class Log
    {
        public static void Info(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public static void Debug(string message, string color = "black")
        {
            message = string.Format("<color={0}>{1}</color>", color, message);
            UnityEngine.Debug.Log(message);
        }

        public static void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public static void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public static void Fatal(Exception e)
        {
            UnityEngine.Debug.LogError(e.ToString());
        }
    }
}
