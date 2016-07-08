using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class DifficultyCalculator : MonoBehaviour
{
    private static DifficultyCalculator instance;

    private IndexedValue<float> spawningPeriodMultiplier;

    private IndexedValue<float> speedMultiplier;

    [SerializeField]
    private TargetConfiguration config;

    public float GetObjectSpawningPeriod(int level)
    {
        float levelMultiplier = GetMultiplierByLevel(level,
            config.SpawnFrequencyByLevelMultiplier, spawningPeriodMultiplier);

        float period = 1f / (config.BaseSpawnFrequency * levelMultiplier);
        return period;
    }

    public float GetSpeed(float scale, int level)
    {
        float sizeMultiplier = GetSizeMultiplier(scale);

        float levelMultiplier = GetMultiplierByLevel(level,
            config.SpeedByLevelMultiplier, speedMultiplier);

        float speed = config.BaseSpeed * sizeMultiplier * levelMultiplier;

        Debug.Assert(speed >= config.BaseSpeed);
        return speed;
    }

    public int GetScore(float scale)
    {
        float sizeMultiplier = GetSizeMultiplier(scale);
        int score = Mathf.RoundToInt(config.BaseScore * sizeMultiplier);
        Debug.Assert(score >= config.BaseScore);
        return score;
    }

    public float GetRandomScale()
    {
        float localScale = Random.Range(config.MinScale, config.MaxScale);
        Debug.Assert(localScale > 0);
        return localScale;
    }

    private float GetSizeMultiplier(float scale)
    {
        if (scale <= 0)
        {
            Debug.LogError("FallingObject.Score: scale <= 0");
            scale = 1f;
        }

        Debug.Assert(config.MaxScale > scale);
        return config.MaxScale / scale;
    }

    private float GetMultiplierByLevel(int level, float configValue,
        IndexedValue<float> cache)
    {
        float levelMultiplier = 0;

        if (cache.Index == level)
        {
            levelMultiplier = cache.Value;
            Debug.Log("Cached value used.");
        }
        else
        {
            levelMultiplier = Mathf.Pow(configValue, level);
            cache.Index = level;
            cache.Value = levelMultiplier;
        }

        return levelMultiplier;
    }

    // Use this for initialization
    private void Start()
    {
        if (instance)
        {
            Debug.LogWarning("DifficultyCalculator.Start(): another instance detected");
            Object.Destroy(gameObject);
            return;
        }

        instance = this;

        if (!config)
        {
            Debug.LogError("DifficultyCalculator.Start(): config is null");
            return;
        }

        spawningPeriodMultiplier = new IndexedValue<float> { Index = -1 };
        speedMultiplier = new IndexedValue<float> { Index = -1 };
    }
}
