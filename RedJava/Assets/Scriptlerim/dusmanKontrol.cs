using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
public class dusmanKontrol : MonoBehaviour
{
    RaycastHit2D ray;       
    public LayerMask layermask;
    public Sprite arkaTaraf;
    public Sprite onTaraf;
    SpriteRenderer spriteRenderer;
    public GameObject kursun;
    GameObject[] gidilecekNoktalar;
    GameObject karakter;
    bool aradakiMesafeKontrol = true;
    bool ileriGeriKontrol = true;
    Vector3 aradakiMesafe;
    float atesZamani = 0;
    int aradakiMesafeSayaci = 0;
    int hiz = 5;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        karakter = GameObject.FindGameObjectWithTag("Player");
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }
    void FixedUpdate()
    {
        beniGordugunde();
        
        if(ray.collider.tag=="Player")
        {
            hiz = 10;
            spriteRenderer.sprite = onTaraf;
            atesEt();
            
        }
        else
        {
            spriteRenderer.sprite = arkaTaraf;
            hiz = 5;
        }
        noktalaraGitGel();
    }
    void beniGordugunde()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position,rayYonum,1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }
    void atesEt()
    {
        atesZamani += Time.deltaTime;
        if(atesZamani>Random.Range(0.2f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }

    void noktalaraGitGel()
    {
        if (aradakiMesafeKontrol)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeKontrol = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeKontrol = true;
            if (ileriGeriKontrol)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ileriGeriKontrol = false;
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ileriGeriKontrol = true;
            }
        }
    }
   public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]
class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        dusmanKontrol script = (dusmanKontrol)target;
        if (GUILayout.Button("Üret", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeni = new GameObject();
            yeni.transform.parent = script.transform;
            yeni.transform.position = script.transform.position;
            yeni.name = script.transform.childCount.ToString();
        }
        // dışarıya bir değişken açmak için editörün içine bunları yazmalıyız.
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        // bu aralığa kadar. 
    }
}
#endif