using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    
    public float speed;
    public float timer = 0;
    public float timerLevelEnd = 50;
    public static  float hurtTimer;
    public static float finalTimer;
    public  float Life = 100;
    public static float currentLife;
    public static bool isLifeCheatOn;
    public static bool isHurting;
    public  bool isDead;
    private Vector3 eulerAngleVelocity;
    private Vector3 posEnZCam;
    public Rigidbody rb;
    public GameObject bullet;
    public GameObject pH;
    public GameObject Cam;
    public AudioClip sndDead;
    public AudioClip sndShoot;
    public  AudioClip sndHurt;
    public RectTransform healthBar;
    public static AudioSource aS;
    public static Animator anim;
    public Image LifebarMove;
    public static int pillowCounter;
    public static int precisionCount;
    public static bool currentLevel1;
    public static bool currentLevel2;
    public static bool currentLevel3;
    public GameObject goodNext;


    void Start()
    {
        GetComponent<Text>();
        LifebarMove = this.transform.FindChild("background").GetComponent<Image>();
        LifebarMove.transform.SetParent(GameObject.Find("Canvas").transform);
        finalTimer = 6000*Time.deltaTime;
        Cursor.visible = false;
        currentLife =Life;
        posEnZCam.x = 5.95f;
        posEnZCam.y = 3.1f;
        posEnZCam.z = -2;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        pH.transform.forward = this.transform.forward;
        Cam.transform.forward = this.transform.forward;
        Cam.transform.position = this.transform.position + posEnZCam;
        Cam.transform.Rotate(16.145f, 0, 0); 
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Y))
        {
            Bunny.count = 8;
            print(Bunny.count);
        }*/
        goodNext.SetActive(false);
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position + Vector3.up * 2);
        LifebarMove.transform.position = pos;
        if(Time.timeScale == 0) LifebarMove.transform.SetParent(this.transform);
        else LifebarMove.transform.SetParent(GameObject.Find("Canvas").transform); ;
        if (isHurting)
        {     
                aS.PlayOneShot(sndHurt);
                   aS.volume = 3;
        }

        if (!isDead && (Input.GetButton("Vertical") || Input.GetButton("Horizontal")))
        {
            Move();
            anim.Play("Move");
        }
        else if(currentLife > 0 && !isHurting)
        {
            anim.Play("Idle");
            rb.velocity = Vector3.zero;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if(timer >0)
        {
            timer--;
        }

        if (hurtTimer > 0)
        {
            hurtTimer--;
            isHurting = false;
            aS.volume = 0.245f ;
        }

        healthBar.sizeDelta = new Vector2(currentLife, healthBar.sizeDelta.y);

        if (currentLife <= 0)
        {
                isDead = true;
                aS.PlayOneShot(sndDead);
                aS.pitch = 0.7f;
                aS.volume = 0.1f;
                anim.Play("Death");
                finalTimer--;
                rb.velocity = Vector3.zero;
            if(finalTimer<=0 || finalTimer >= 1000)
            { 
                SceneManager.LoadScene("lose", LoadSceneMode.Single);
            }
         }

        if (currentLife <= 0 && currentLevel3 == true)
        {
            isDead = true;
            aS.PlayOneShot(sndDead);
            aS.pitch = 0.7f;
            aS.volume = 0.1f;
            anim.Play("Death");
            finalTimer--;
            if (finalTimer <= 0)
            {
                SceneManager.LoadScene("loseAds", LoadSceneMode.Single);
            }
        }

        if (Bunny.count == 2)
        {
            finalTimer--;
            //print(finalTimer);
            if (finalTimer <= 0)
            {
                goodNext.SetActive(true);
                StartCoroutine(Winner1());    
            }
        }

        if (Bunny.count == 7)
        {
            finalTimer--;
            if (finalTimer <= 0)
            {
                goodNext.SetActive(true);
                StartCoroutine(Winner2());     
            }
        }

        if (Elephant.isDead && Bunny.count == 0)
        {
            finalTimer--;
            if (finalTimer <= 0)
            {
                goodNext.SetActive(true);
                StartCoroutine(Winner3());
            }
        }
        print(Bunny.count);
    }

    IEnumerator Winner1()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level2Next", LoadSceneMode.Single);
        currentLevel1 = false;
        currentLevel2 = true;
        currentLevel3 = false;
    }

    IEnumerator Winner2()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level3Next", LoadSceneMode.Single);
        currentLevel1 = false;
        currentLevel2 = false;
        currentLevel3 = true;
    }

    IEnumerator Winner3()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("win", LoadSceneMode.Single);
    }

    public void Move()
    {
        if (currentLife > 0)
        {
           rb.velocity = speed * transform.forward* Input.GetAxis("Vertical");
           if(Input.GetAxis("Horizontal") < 0)
           {
              eulerAngleVelocity.y = -130;

           }
           else if (Input.GetAxis("Horizontal")> 0)
           {
               eulerAngleVelocity.y =130;
           }
           else
           {
               eulerAngleVelocity.y = 0;
           }
       
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public static void Hurt(float damage)
    {
        if (hurtTimer <= 0)
        {

            anim.Play("Death");
            isHurting = true;
            currentLife -= damage;
            hurtTimer = 100;
        }
    }

    public void Shoot()
    {
        if (timer <= 0 && Time.timeScale > 0)
        {
            aS.PlayOneShot(sndShoot);
            GameObject b = GameObject.Instantiate(bullet);
            b.transform.position = pH.transform.position;
            b.transform.forward = transform.forward;
            if (!isLifeCheatOn)
            {
                timer = 50;
            }
        }
    }
}