using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class karakterKontrol : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] yurumeAnim;
    public Sprite[] ziplamaAnim;
    
    public Text Textcan;
    public Text altinText;
    public Image SiyahArkaPlan;
    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;
    Vector3 vect;
    Vector3 kameraSonPoz;
    Vector3 kameraIlkPoz;
    GameObject kamera;
    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float beklemeAnimYurume = 0;
    
    float SiyahArkaPlanSayaci = 0;
    float anaMenuyeDonZaman = 0;
    //float beklemeAnimZiplama = 0;
    bool ziplamaKontrol = true;
   
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int can = 100;
    int altinSayaci = 0;


    void Start()
    {
        Time.timeScale = 1;
        SiyahArkaPlan.gameObject.SetActive(false);
        fizik = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPoz = kamera.transform.position - transform.position;
        Textcan.text ="CAN:"+can;
        altinText.text = "30-";
        if(SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);
        }
    }

     void Update()
    {
                                                                                                                                                   
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if(ziplamaKontrol)
            {
                fizik.velocity = new Vector3(0, 0, 0);
                fizik.AddForce(new Vector2(0, 500));
                ziplamaKontrol = false;
            }
            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        ziplamaKontrol = true; 
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Kursun")
        {
            can--;
            Textcan.text = "CAN:" + can;

        }
        if(col.gameObject.tag == "Dusman")
        {
            can -= 10;
            Textcan.text = "CAN:" + can;
        }
        if(col.gameObject.tag=="testere")
        {
           
                can -= 10;
            
            Textcan.text = "CAN:" + can;
        }
        if (col.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       if(col.gameObject.tag=="canver")
        {
            if(can!=100)
            {
                can += 10;
                Textcan.text = "CAN:" + can;
            }
            col.GetComponent<canVer>().enabled = true;
            col.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(col.gameObject, 2);

        }
        if (col.gameObject.tag == "altin")
        {
            Destroy(col.gameObject);
            altinSayaci++;
            altinText.text = "30-" + altinSayaci;

        }
        if (col.gameObject.tag == "su")
        {
            transform.Rotate(0, 0, -45);
            can = 0;
        }
      
    }
    void oyunBitti()
    {

    }
    void FixedUpdate()
    {
        
        karakterHareket();
        Animasyon();
        if (can <= 0)
        {
            Time.timeScale = 0.4f;
            SiyahArkaPlanSayaci += 0.03f;
            Textcan.enabled = false;
            SiyahArkaPlan.gameObject.SetActive(true);
            SiyahArkaPlan.color = new Color(0,0,0,SiyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if(anaMenuyeDonZaman>1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }
       
    }
    void LateUpdate()
    {
        kameraKontrol();
    }
    void karakterHareket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vect = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vect;
    }
    void kameraKontrol()
    {
        kameraSonPoz = kameraIlkPoz + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position,kameraSonPoz,0.1f);
    }
    void Animasyon()
    {
        if(ziplamaKontrol)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac];
                    beklemeAnimSayac++;
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }
            }
            else if (horizontal > 0)
            {
                beklemeAnimYurume += Time.deltaTime;
                if (beklemeAnimYurume > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac];
                    yurumeAnimSayac++;
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    beklemeAnimYurume = 0;
                }
                transform.localScale = new Vector3(1,1,1);

            }
            else if (horizontal < 0)
            {

                beklemeAnimYurume += Time.deltaTime;
                if (beklemeAnimYurume > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac];
                    yurumeAnimSayac++;
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    beklemeAnimYurume = 0;
                }
                transform.localScale = new Vector3(-1,1,1);
            }
        }
      
        else
        {
           
                if (fizik.velocity.y > 0)
                {
                    spriteRenderer.sprite = ziplamaAnim[0];
                }
                if (fizik.velocity.y < 0)
                {
                    spriteRenderer.sprite = ziplamaAnim[1];
                }
                if(horizontal>0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                    if(horizontal<0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

        }
    }
}
