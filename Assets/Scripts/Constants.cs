using System.IO;
using UnityEngine;

public static class Constants
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");
#elif UNITY_EDITOR
    public static readonly string SavePath = Path.Combine(Application.dataPath, "save.json");
#endif

    public const int MinStar = 1;
    public const int MaxStar = 2;

    public const int MaxPart = 4;
    
    public const int ProductTypeCount = 4;

    public const string EmptyOutfit = "Empty";
}