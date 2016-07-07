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

    /// <summary>
    /// A singleton instance.
    /// </summary>
    public static Configuration Instance { get; set; }


    public string ActionAxisName { get { return actionAxisName; } }

    public int PointsToNextLevel { get { return pointsToNextLevel; } }

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
