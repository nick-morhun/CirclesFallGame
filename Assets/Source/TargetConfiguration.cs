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
    [Range(.1f, 100f)]
    private float baseSpawnFrequency;

    [SerializeField]
    [Range(.1f, 10f)]
    private float baseSpeed;

    [SerializeField]
    [Range(1.01f, 5f)]
    private float spawnFrequencyByLevelMultiplier;

    [SerializeField]
    [Range(1.01f, 5f)]
    private float speedByLevelMultiplier;

    [SerializeField]
    [Range(.1f, 100f)]
    private float minScale;

    [SerializeField]
    [Range(.1f, 100f)]
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

    public float MinScale { get { return Mathf.Min(maxScale, minScale); } }

    public float MaxScale { get { return Mathf.Max( maxScale, minScale); } }


}
