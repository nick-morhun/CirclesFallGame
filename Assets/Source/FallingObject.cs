using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class FallingObject : MonoBehaviour
{
    private static long objectsCreated = 0L;

    private bool isInitialized = false;

    private float speed = 0f;

    private int score = 0;

    public float Speed
    {
        get
        {
            if (!isInitialized)
            {
                Debug.LogError("FallingObject.Speed: object was not initialized");
            }
            return speed;
        }
    }

    public int Score
    {
        get
        {
            if (!isInitialized)
            {
                Debug.LogError("FallingObject.Score: object was not initialized");
            }
            return score;
        }
    }

    public int LevelCreated { get; private set; }

    /// <summary>
    /// Initializes this object.
    /// </summary>
    public void Initialize(float scale, int levelCreated)
    {
        if (isInitialized)
        {
            return;
        }

        if (scale <= 0)
        {
            Debug.LogError("FallingObject.Score: scale <= 0");
            scale = 1f;
        }

        LevelCreated = levelCreated;
        gameObject.name = "Object " + objectsCreated + " Level " + levelCreated;
        //TODO: use level in formulas

        TargetConfiguration config = Configuration.Instance.Target;
        Debug.Assert(config.MaxScale > scale);

        float sizeMultiplier = (config.MaxScale / scale);
        speed = config.BaseSpeed * sizeMultiplier;
        score = Mathf.RoundToInt(config.BaseScore * sizeMultiplier);

        Debug.Assert(speed >= config.BaseSpeed);
        Debug.Assert(score >= config.BaseScore);
        isInitialized = true;
        objectsCreated++;
        //Debug.Log("FallingObject.Initialize(): speed = " + speed + " score = " + score);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -Speed * Time.fixedDeltaTime, 0);
    }
}
