using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
public class testere : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeKontrol = true;
    bool ileriGeriKontrol = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayaci = 0;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }
    void FixedUpdate()
    {
        transform.Rotate(0,0,15);
        noktalaraGitGel();
    }
    void noktalaraGitGel()
    {
        if (aradakiMesafeKontrol)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeKontrol = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;
        if(mesafe<0.5f)
        {
            aradakiMesafeKontrol = true;
            if(ileriGeriKontrol)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
            if(aradakiMesafeSayaci==gidilecekNoktalar.Length-1)
            {
                ileriGeriKontrol = false;
            }
            else if(aradakiMesafeSayaci==0)
            {
                ileriGeriKontrol = true;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position,1);
        }
        for (int i = 0; i <transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]
class testereEditor :Editor
{
    public override void OnInspectorGUI()
    {
        testere script = (testere)target;
        if (GUILayout.Button("Üret",GUILayout.MinWidth(100),GUILayout.Width(100)))
        {
            GameObject yeni = new GameObject();
            yeni.transform.parent = script.transform;
            yeni.transform.position = script.transform.position;
            yeni.name = script.transform.childCount.ToString();
        }
    }
}
#endif 