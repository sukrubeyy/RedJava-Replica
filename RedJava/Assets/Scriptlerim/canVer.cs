using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canVer : MonoBehaviour
{
    public Sprite []animasyonKareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonKareleriSayaci = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

  
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.1f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            if(animasyonKareleriSayaci==animasyonKareleri.Length)
            {
                animasyonKareleriSayaci =animasyonKareleri.Length-1;
            }
            zaman = 0;
        }
    }
}
