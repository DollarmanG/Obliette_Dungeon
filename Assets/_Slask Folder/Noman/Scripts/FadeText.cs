using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    //[SerializeField] private Material material;
    //[SerializeField] private Font font;
    [SerializeField] private TMP_FontAsset fontAsset;

    void Start()
    {
        
    }

    
    void Update()
    {
        Debug.Log(fontAsset.material.GetFloat("_FaceDilate"));
        //Debug.Log(font.material.GetFloat("_FaceDilate"));
        //Debug.Log(material.GetFloat("_FaceDilate"));
    }
}
