using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour
{
    private Game game;

    [SerializeField]
    private GameGui gameGui;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private CirclesManager circlesManager;

    // Use this for initialization
    private void Start()
    {
        if (!gameGui)
        {
            Debug.LogError("GameController.Start(): gameGui is null");
            return;
        }

        if (!inputManager)
        {
            Debug.LogError("GameController.Start(): inputManager is null");
            return;
        }

        if (!circlesManager)
        {
            Debug.LogError("GameController.Start(): circlesManager is null");
            return;
        }

        game = NewGame();
        gameGui.UpdateUI(game);
        inputManager.Touch += OnTouch;
        SetupAssets();
        StartCoroutine(Clock());
    }

    public void OnTouch(TouchEventArgs args)
    {
        RaycastHit2D hit = Physics2D.Raycast(args.PointerWorldPosition, Vector2.zero);

        if (hit.collider && hit.collider.tag == Configuration.Instance.Target.Tag)
        {
            var obj = hit.collider.GetComponent<FallingObject>();

            game.Score += obj.Score;

            int requiredScore = Configuration.Instance.PointsToNextLevel * (game.Level + 1);
            if (game.Score >= requiredScore)
            {
                game.Level++;
                circlesManager.NextLevel(game.Level);
            }
            Debug.Log("An object destroyed");
            circlesManager.RecycleObject(obj);
            gameGui.UpdateUI(game);
        }
    }

    private void SetupAssets()
    {
        StartCoroutine(LoadObjectPrefabFromBundle(Strings.AssetBundlesBasePath
            + "falling_objects_bundle", "Circle"));
    }

    private IEnumerator LoadObjectPrefabFromBundle(string url, string prefabName)
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

            AssetBundle assetBundle = www.assetBundle;

            if (!assetBundle)
            {
                Debug.LogError("Failed to load assetbundle: " + url);
                yield break;
            }

            GameObject go = assetBundle.LoadAsset<GameObject>(prefabName);
            FallingObject circlePrefab = go.GetComponent<FallingObject>();
            assetBundle.Unload(false);
            circlesManager.Initialize(circlePrefab);
            circlesManager.NextLevel(0);
        }
    }

    private IEnumerator Clock()
    {
        while (true)
        {
            gameGui.UpdateUI(game);
            yield return new WaitForSeconds(1);
            game.TotalSeconds++;
        }
    }

    private static Game NewGame()
    {
        return new Game();
    }
}
