using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class RandomColoredCircle : MonoBehaviour
{
    private bool isInitialized = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int[] textureSizes;

    /// <summary>
    /// Initializes this circle's scale and color.
    /// </summary>
    public void Initialize(float scale, TextureCreator textureCreator)
    {
        if (!textureCreator)
        {
            Debug.LogError("RandomColoredCircle.Initialize(): textureCreator is null");
            return;
        }

        if (isInitialized)
        {
            return;
        }

        Color color = new Color(Random.value, Random.value, Random.value);

        // Fall back
        if (textureSizes.Length == 0)
        {
            transform.localScale = new Vector3(scale, scale, 1);
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            isInitialized = true;
            return;
        }

        int size = GetTextureSize(scale);

        // Create texture
        textureCreator.TextureSize = size;
        textureCreator.color = color;
        Texture2D texture = textureCreator.Generate();

        // Set texture to the object
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(.5f, .5f));
        spriteRenderer.sprite.name = "Generated Sprite";

        // Adjust scale
        transform.localScale = new Vector3(1, 1, 1);
        var collider = GetComponent<CircleCollider2D>();
        collider.radius = 0.5f * size / spriteRenderer.sprite.pixelsPerUnit;
        isInitialized = true;
    }

    private void OnDestroy()
    {
        Object.Destroy(spriteRenderer.sprite.texture);
    }

    private int GetTextureSize(float scale)
    {
        if (textureSizes.Length == 0)
        {
            Debug.LogWarning("RandomColoredCircle.GetTextureSize(): textureSizes is empty");
            return 32;
        }

        TargetConfiguration config = Configuration.Instance.Target;
        scale = Mathf.Clamp(scale, config.MinScale, config.MaxScale);

        float scaleToMax = scale / config.MaxScale;
        int sizeIndex = (int)Mathf.Clamp(Mathf.Round(scaleToMax * textureSizes.Length),
            0, textureSizes.Length - 1);
        return textureSizes[sizeIndex];
    }
}
