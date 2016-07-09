using UnityEngine;

public static class Strings
{
    public static readonly string LevelText = "Level: {0}";

    public static readonly string ScoreText = "Score: {0}";

    public static readonly string TimeText = "{0}:{1:D2}:{2:D2}";

    public static string AssetBundlesBasePath
    {
        get
        {
            return "file://" + Application.streamingAssetsPath + "/AssetBundles/Windows/";
        }
    }

    public static readonly string ObjectPrefabName = "TexturedCircle";
}
