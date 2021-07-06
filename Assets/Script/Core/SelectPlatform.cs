using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlatform : MonoBehaviour
{
    [SerializeField] Canvas pcCanvas;
    [SerializeField] Canvas androidCanvas;

    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            androidCanvas.gameObject.SetActive(false);
            
        }        
    }

}
