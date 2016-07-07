using UnityEngine;
using System.Collections;

// TODO: stop falling
[DisallowMultipleComponent]
public class FallingObject : MonoBehaviour
{

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

    /// <summary>
    /// Initializes this object.
    /// </summary>
    public void Initialize(float scale)
    {
        if (scale <= 0)
        {
            Debug.LogError("FallingObject.Score: scale <= 0");
            scale = 1f;
        }

        TargetConfiguration config = Configuration.Instance.Target;
        Debug.Assert(config.MaxScale > scale);

        float sizeMultiplier = (config.MaxScale / scale);
        speed = config.BaseSpeed * sizeMultiplier;
        score = Mathf.RoundToInt(config.BaseScore * sizeMultiplier);

        Debug.Assert(speed >= config.BaseSpeed);
        Debug.Assert(score >= config.BaseScore);
        isInitialized = true;
        Debug.Log("FallingObject.Initialize(): speed = " + speed + " score = " + score);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -Speed * Time.fixedDeltaTime, 0);
    }
}
