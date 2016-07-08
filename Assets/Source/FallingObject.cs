using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class FallingObject : MonoBehaviour
{
    private static long objectsCreated = 0L;

    private bool isInitialized = false;

    private float speed = 0f;

    private int score = 0;

    protected float Speed
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
    public void Initialize(DifficultyCalculator calc, float scale, int levelCreated)
    {
        if (isInitialized)
        {
            return;
        }

        LevelCreated = levelCreated;
        gameObject.name = "Object " + objectsCreated + " Level " + levelCreated;

        speed = calc.GetSpeed(scale, levelCreated);
        score = calc.GetScore(scale);

        isInitialized = true;
        objectsCreated++;
        Debug.Log("FallingObject.Initialize(): speed = " + speed + " score = " + score);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -Speed * Time.fixedDeltaTime, 0);
    }
}
