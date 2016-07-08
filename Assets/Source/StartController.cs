using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class StartController : MonoBehaviour
{
    public void StartGame()
    {
        LoadSceneFromStreamingAssetsWindows("scenes_bundle", "main");
    }

    // For standalone Windows player.
    private void LoadSceneFromStreamingAssetsWindows(string bundleName, string sceneName)
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer
            && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Debug.LogError("Current platform is not supported: " + Application.platform);
            return;
        }

        StartCoroutine(LoadScene(Strings.AssetBundlesBasePath + bundleName, sceneName));
    }

    private IEnumerator LoadScene(string url, string sceneName)
    {
        int version = 0;

        while (!Caching.ready)
            yield return null;

        using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }

            AssetBundle assetBundle = www.assetBundle;  // scenes are loaded here

            if (!assetBundle)
            {
                Debug.LogError("Failed to load assetbundle: " + url);
                yield break;
            }

            yield return SceneManager.LoadSceneAsync(sceneName);
            assetBundle.Unload(false);
        }
    }
}
