using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CirclesManager : MonoBehaviour
{
    private Pull<FallingObject> pull;

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

    public void NextLevel(int level)
    {
        StopAllCoroutines();
        pull.Clear();
        this.cachedLevel = level;
        StartCoroutine(CreateObjectAtRandomTime());
    }

    public void RecycleObject(FallingObject fallingObject)
    {
        fallingObject.enabled = false;
        fallingObject.transform.position = spawningStart.position;
        pull.Put(fallingObject);
    }

    // Use this for initialization
    private void Start()
    {
        if (!prefab)
        {
            Debug.LogError("CirclesManager.NextLevel(): prefab is null");
            return;
        }

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

        pull = new Pull<FallingObject>(prefab);
        stopper.ObjectStopped += OnObjectStopped;
        NextLevel(0);
    }

    private IEnumerator CreateObjectAtRandomTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / Configuration.Instance.Target.BaseSpawnFrequency);

            FallingObject nextCircle = pull.Get();
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
        Debug.Log("An object stopped");
        pull.Put(args.FallingObject);
    }
}
