using UnityEngine;

public static class Strings
{
    public static string LevelText = "Level: {0}";

    public static string ScoreText = "Score: {0}";

    public static string TimeText = "{0}:{1:D2}:{2:D2}";

    public static string AssetBundlesBasePath
    {
        get
        {
            return "file://" + Application.streamingAssetsPath + "/AssetBundles/Windows/";
        }
    }
}
