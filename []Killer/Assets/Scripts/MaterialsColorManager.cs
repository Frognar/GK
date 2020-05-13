using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 * 
 * Allow extract base color from material, avg color from texture and create gradient color.
 * Used for calculate VFX gradient color.
 */
public class MaterialsColorManager : MonoBehaviour
{
    #region Singleton

    public static MaterialsColorManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    /**
     * Create gradient for VFX ImpactEffect.
     */
    public Gradient NiceGradientForImpactEffect(Material material)
    {
        Texture texture = GetMaterialMainTexture(material);
        Texture2D texture2D = TextureToTexture2D(texture);
        Color textureAvgColor = Texture2DAvgColor(texture2D);
        Color color = GetMaterialBaseColor(material);
        Color newColor = AddColorsWithWeights(textureAvgColor, color, 1f, 0.5f);

        if (newColor.Equals(new Color()))
            return null;

        Gradient gradient;
        gradient = new Gradient();

        GradientColorKey[] colorKey;
        colorKey = new GradientColorKey[1];
        colorKey[0].color = newColor;
        colorKey[0].time = 0f;

        GradientAlphaKey[] alphaKey;
        alphaKey = new GradientAlphaKey[5];
        alphaKey[0].alpha = 0f;
        alphaKey[0].time = 0f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 0.25f;
        alphaKey[2].alpha = 0.7843137f;
        alphaKey[2].time = 0.5f;
        alphaKey[3].alpha = 0.5882353f;
        alphaKey[3].time = 0.75f;
        alphaKey[4].alpha = 0f;
        alphaKey[4].time = 1f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }

    /**
     * Create gradient for VFX EnemyDeathEffect.
     */
    public Gradient NiceGradientForEnemyDeathEffect(Material material)
    {
        Texture texture = GetMaterialMainTexture(material);
        Texture2D texture2D = TextureToTexture2D(texture);
        Color textureAvgColor = Texture2DAvgColor(texture2D);
        Color color = GetMaterialBaseColor(material);
        Color newColor = AddColorsWithWeights(textureAvgColor, color, 2f, 0.5f);

        if (newColor.Equals(new Color()))
            return null;

        Gradient gradient;
        gradient = new Gradient();

        GradientColorKey[] colorKey;
        colorKey = new GradientColorKey[1];
        colorKey[0].color = newColor;
        colorKey[0].time = 0f;

        GradientAlphaKey[] alphaKey;
        alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }

    public Color AddColorsWithWeights(Color colorA, Color colorB, float colorAWeight, float colorBWeight)
    {
        if (colorA.Equals(new Color()) && colorB.Equals(new Color()))
            return new Color();

        float r = 0f, g = 0f, b = 0f;

        if (!colorA.Equals(new Color()))
        {
            r += colorA.r * colorAWeight / 2f;
            g += colorA.g * colorAWeight / 2f;
            b += colorA.b * colorAWeight / 2f;
        }

        if(!b.Equals(new Color()))
        {
            r += colorB.r * colorBWeight / 2f;
            g += colorB.g * colorBWeight / 2f;
            b += colorB.b * colorBWeight / 2f;
        }

        return new Color(r, g, b);
    }

    public Color GetMaterialBaseColor(Material material)
    {
        if (material == null)
            return new Color();

        return material.GetColor("_BaseColor");
    }

    public Texture GetMaterialMainTexture(Material material)
    {
        if (material == null)
            return null;

        return material.GetTexture("_MainTex");
    }

    public Texture2D TextureToTexture2D(Texture texture)
    {
        if (texture == null)
            return null;

        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        return texture2D;
    }

    public Color Texture2DAvgColor(Texture2D texture2D)
    {
        if (texture2D == null)
            return new Color();

        int pixelsCount = texture2D.width * texture2D.height;
        float r = 0f, g = 0f, b = 0f;

        foreach(Color pixel in texture2D.GetPixels())
        {
            r += pixel.r;
            g += pixel.g;
            b += pixel.b;
        }

        r /= pixelsCount;
        g /= pixelsCount;
        b /= pixelsCount;
        return new Color(r, g, b);
    }

}
