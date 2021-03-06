using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altinKontrol : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
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
        if (zaman > 0.04f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            if (animasyonKareleriSayaci == animasyonKareleri.Length)
            {
                animasyonKareleriSayaci = 0;
            }
            zaman = 0;
        }
    }
}
