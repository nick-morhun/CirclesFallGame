using UnityEngine;
using System.Collections;

public class CirclesManager : MonoBehaviour
{
    private Pool<FallingObject> pool;

    private bool isInitialized = false;

    /// <summary>
    /// Derived from Game.
    /// </summary>
    private int cachedLevel;

    [SerializeField]
    private FallingObject prefab;

    [SerializeField]
    private Transform gameLevel;

    [SerializeField]
    private Transform spawningStart;

    [SerializeField]
    private Transform spawningEnd;

    [SerializeField]
    private ObjectStopper stopper;

    [SerializeField]
    private DifficultyCalculator difficultyCalc;

    [SerializeField]
    private TextureCreator textureCreator;

    public void Initialize(FallingObject prefab)
    {
        if (!prefab)
        {
            Debug.LogError("CirclesManager.Initialize(): prefab is null");
            return;
        }

        this.prefab = prefab;
        this.isInitialized = true;
        pool = new Pool<FallingObject>(this.prefab);
    }

    public void NextLevel(int level)
    {
        if (!isInitialized)
        {
            Debug.LogError("CirclesManager.NextLevel(): isInitialized is false");
            return;
        }

        StopAllCoroutines();
        pool.Clear();
        this.cachedLevel = level;
        StartCoroutine(CreateObjectAtRandomTime());
    }

    public void RecycleObject(FallingObject fallingObject)
    {
        fallingObject.enabled = false;
        fallingObject.transform.position = spawningStart.position;
        CheckAndRecycle(fallingObject);
    }

    // Use this for initialization
    private void Start()
    {
        if (!gameLevel)
        {
            Debug.LogError("CirclesManager.Start(): gameLevel is null");
            return;
        }

        if (!stopper)
        {
            Debug.LogError("CirclesManager.Start(): stopper is null");
            return;
        }

        if (!difficultyCalc)
        {
            Debug.LogError("CirclesManager.Start(): difficultyCalc is null");
            return;
        }

        if (!textureCreator)
        {
            Debug.LogError("CirclesManager.Start(): textureCreator is null");
            return;
        }

        stopper.ObjectStopped += OnObjectStopped;
    }

    private IEnumerator CreateObjectAtRandomTime()
    {
        float period = difficultyCalc.GetObjectSpawningPeriod(cachedLevel);

        while (true)
        {
            yield return new WaitForSeconds(period);

            FallingObject nextCircle = pool.Get();
            nextCircle.transform.SetParent(gameLevel);
            InitializeCircleComponents(nextCircle);
        }
    }

    private void InitializeCircleComponents(FallingObject fallingObject)
    {
        float localScale = difficultyCalc.GetRandomScale();

        fallingObject.Initialize(difficultyCalc, localScale, cachedLevel);
        fallingObject.enabled = true;

        float spawnPointX = Random.Range(spawningStart.position.x, spawningEnd.position.x);
        fallingObject.transform.position = new Vector3(spawnPointX, spawningStart.position.y, 0f);

        var coloredCircle = fallingObject.GetComponent<RandomColoredCircle>();
        coloredCircle.Initialize(localScale, textureCreator);
    }

    private void OnObjectStopped(ObjectStoppedEventArgs args)
    {
        CheckAndRecycle(args.FallingObject);
    }

    private void CheckAndRecycle(FallingObject fallingObject)
    {
        if (fallingObject.LevelCreated == cachedLevel)
        {
            Debug.Log("An object put to Pool");
            pool.Put(fallingObject);
        }
        else
        {
            Debug.LogWarning("An object destroyed");
            Object.Destroy(fallingObject.gameObject);
        }
    }
}
