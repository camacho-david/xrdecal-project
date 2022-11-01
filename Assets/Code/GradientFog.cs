using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GradientFog : MonoBehaviour
{

    public Material material;
    public bool useUnityGradient;
    public Gradient gradient;
    public float fogIntensity = 1;
    public float fogStart = 10;
    public float fogEnd = 1000;
    Texture2D gradTex;

    void OnValidate()
    {
        if (useUnityGradient)
        {
            gradTex = GradientToTexture(gradient);
            material.SetTexture("_ColorRamp", gradTex);
        }
        material.SetFloat("_FogIntensity", fogIntensity);
        material.SetFloat("_FogStart", fogStart);
        material.SetFloat("_FogEnd", fogEnd);
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    void Start()
    {
        if (useUnityGradient)
        {
            gradTex = GradientToTexture(gradient);
            material.SetTexture("_ColorRamp", gradTex);

        }
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }

    public void BakeGradient()
    {
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/", "-");
        date = date.Replace(" ", "_");
        date = date.Replace(":", "-");
        string texturePath = "/FogGradients/Gradient" + date + ".png";
        if (!Directory.Exists(Application.dataPath + "/FogGradients"))
        {
            Directory.CreateDirectory(Application.dataPath + "/FogGradients");
        }
        File.WriteAllBytes(Application.dataPath + texturePath, gradTex.EncodeToPNG());
        AssetDatabase.Refresh();
        useUnityGradient = false;
        gradTex = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets" + texturePath, typeof(Texture2D));
        material.SetTexture("_ColorRamp", gradTex);
    }

    Texture2D GradientToTexture(Gradient g)
    {
        Texture2D tex = new Texture2D(128, 1);
        for (int i = 0; i < 128; i++)
        {
            tex.SetPixel(i, 0, gradient.Evaluate(((float)i) / 128.0f));
        }
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.alphaIsTransparency = true;

        tex.Apply();
        return tex;
    }
}
