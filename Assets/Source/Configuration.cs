using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Configuration : MonoBehaviour
{
    [SerializeField]
    private TargetConfiguration target;

    [SerializeField]
    private string actionAxisName;

    [SerializeField]
    private int pointsToNextLevel;

    [SerializeField]
    private int ojectsCacheSize;

    /// <summary>
    /// A singleton instance.
    /// </summary>
    public static Configuration Instance { get; set; }


    public string ActionAxisName { get { return actionAxisName; } }

    public int PointsToNextLevel { get { return pointsToNextLevel; } }

    /// <summary>
    /// A number of objects that should stay in cache.
    /// </summary>
    public int ObjectsCacheBufferSize { get { return ojectsCacheSize; } }

    public TargetConfiguration Target { get { return target; } }

    // Use this for initialization
    private void Start()
    {
        if (Instance)
        {
            Debug.LogWarning("Configuration.Start(): another instance detected");
            Object.Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
