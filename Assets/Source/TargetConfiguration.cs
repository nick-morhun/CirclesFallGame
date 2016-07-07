using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class TargetConfiguration : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    [SerializeField]
    private float baseScore;

    [SerializeField]
    private float baseSpawnFrequency;

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private float spawnFrequencyByLevelMultiplier;

    [SerializeField]
    private float speedByLevelMultiplier;

    [SerializeField]
    private float minScale;

    [SerializeField]
    private float maxScale;


    public string Tag { get { return tagName; } }

    /// <summary>
    /// When an object's scale equals to MaxScale.
    /// </summary>
    public float BaseScore { get { return baseScore; } }

    /// <summary>
    /// When an object's scale equals to MaxScale, at level 0.
    /// </summary>
    public float BaseSpawnFrequency { get { return baseSpawnFrequency; } }

    /// <summary>
    /// When an object's scale equals to MaxScale, at level 0.
    /// </summary>
    public float BaseSpeed { get { return baseSpeed; } }

    public float SpawnFrequencyByLevelMultiplier { get { return spawnFrequencyByLevelMultiplier; } }

    public float SpeedByLevelMultiplier { get { return speedByLevelMultiplier; } }

    public float MinScale { get { return minScale; } }

    public float MaxScale { get { return maxScale; } }


}
