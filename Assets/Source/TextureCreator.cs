using UnityEngine;

public class TextureCreator : MonoBehaviour
{
    private RenderTexture renderTexture;

#if UNITY_EDITOR
    [SerializeField]
    private bool TestNow = false;
#endif

    [SerializeField]
    private Texture2D ResultTexture;

    public Color color;

    public Material material;

    public int TextureSize = 256;

    public Texture2D Generate()
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(TextureSize, TextureSize, 0);
        }

        ResultTexture = ProcessTexture();
        return ResultTexture;
    }

#if UNITY_EDITOR

    // For testing
    private void Update()
    {
        if (TestNow)
        {
            TestNow = false;
            Generate();
        }
    }
#endif

    private Texture2D ProcessTexture()
    {
        var mat = material;
        var texture2D = new Texture2D(TextureSize, TextureSize);
        mat.color = color;
        mat.mainTexture = texture2D;

        // Apply shader
        Graphics.Blit(mat.mainTexture, renderTexture, mat);

        mat.mainTexture = null;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0, false);
        texture2D.Apply();
        return texture2D;
    }
}