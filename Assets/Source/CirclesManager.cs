using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            Debug.LogError("GameController.Start(): gameLevel is null");
            return;
        }

        if (!stopper)
        {
            Debug.LogError("GameController.Start(): stopper is null");
            return;
        }

        stopper.ObjectStopped += OnObjectStopped;
    }

    private IEnumerator CreateObjectAtRandomTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / Configuration.Instance.Target.BaseSpawnFrequency);

            FallingObject nextCircle = pool.Get();
            InitializeCircleComponents(nextCircle);
        }
    }

    private void InitializeCircleComponents(FallingObject fallingObject)
    {
        var config = Configuration.Instance.Target;
        float localScale = Random.Range(config.MinScale, config.MaxScale);
        fallingObject.Initialize(localScale, cachedLevel);
        var coloredCircle = fallingObject.GetComponent<RandomColoredCircle>();
        coloredCircle.Initialize(localScale);
        fallingObject.enabled = true;

        float spawnPointX = Random.Range(spawningStart.position.x, spawningEnd.position.x);
        fallingObject.transform.position = new Vector3(spawnPointX, spawningStart.position.y, 0f);
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
