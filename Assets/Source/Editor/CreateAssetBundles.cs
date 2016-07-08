using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles 4 Windows")]
    private static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles/Windows",
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
}
