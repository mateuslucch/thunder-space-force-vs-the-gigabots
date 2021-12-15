using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CRT : MonoBehaviour
{
    #region Variables
    public Shader curShader;
    public float Distortion = 0.1f;
    public float InputGamma = 2.4f;
    public float OutputGamma = 2.2f;
    public Vector2 TextureSize = new Vector2(1000f, 1000f);
    private Material curMaterial;
    public Vector3 RGB1 = new Vector3(1.0f, 0.7f, 1.0f); //float3(1.0f, 0.7f, 1.0f)
    public Vector3 RGB2 = new Vector3(1.0f, 0.7f, 1.0f);//float3(0.7f, 1.0f, 0.7f)
    [SerializeField] Camera mainCamera;

    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    void Start()
    {
        if (mainCamera.pixelWidth - mainCamera.pixelHeight != TextureSize.x)
        {
            //TextureSize = new Vector2(mainCamera.pixelWidth - mainCamera.pixelHeight, mainCamera.pixelWidth - mainCamera.pixelHeight);
        }
        /* if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        } */

    }

    private void Update()
    {
        if (mainCamera.pixelWidth - mainCamera.pixelHeight != TextureSize.x)
        {
            //TextureSize = new Vector2(mainCamera.pixelWidth - mainCamera.pixelHeight, mainCamera.pixelWidth - mainCamera.pixelHeight);
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetFloat("_Distortion", Distortion);
            material.SetFloat("_InputGamma", InputGamma);
            material.SetFloat("_OutputGamma", OutputGamma);
            material.SetVector("_TextureSize", TextureSize);
            material.SetVector("_RGB1", RGB1);
            material.SetVector("_RGB2", RGB2);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }

    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}