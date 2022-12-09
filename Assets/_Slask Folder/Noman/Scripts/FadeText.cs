using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset fontAsset;
    [SerializeField][Range(-1,0)] private float dilateSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        dilateSpeed = fontAsset.material.GetFloat("_FaceDilate");
    }
}
