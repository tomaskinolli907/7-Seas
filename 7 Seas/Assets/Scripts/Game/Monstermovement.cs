using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//This script handled movement and enemy health because movement needs to be halted and an animation played every time the enemy takes damage


public class Monstermovement : MonoBehaviour
{
    public int moveTowardspeed = 10;
    public float height;
    public float attackHeight=-10;
    public bool isMoving;
    public int sideMovingspeed=3;
    private int timesMoved;//times the monster has moved to the left
    private int timesAttacked;//times the monster has used its attack
    private bool justHit;//check if monster was hit during in its current lane
    float x;//the x vector of the monsters position
    bool movingForward;
    bool movingBackward;
    public Animation anim;
    public float health;
    public float maxHealth;
    public Slider slider;

    private GameObject explosion;
    private ParticleSystem explosionEffect;
    private AudioSource explosionSFX;
    //public bool istriggered;
    Collider m_Collider;
    public int cannonDmg;//amount of damage cannons do with each hit
   // Vector3 cameraInitialPosition;
   // public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
   // public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        movingForward = true;
        movingBackward = false;
        timesMoved = 0;
        timesAttacked = 0;
        health = maxHealth;
        slider.value = 100;
        //istriggered = false;
        m_Collider = GetComponent<Collider>();
        justHit = false;
        anim.Play("Appear");
        PlayerPrefs.SetInt("DamageDoneMonster", 0);
        PlayerPrefs.SetString("MonsterStatus", "Alive");
        PlayerPrefs.Save();

        explosion = GameObject.Find("ScoreExplosion");
        explosionEffect = explosion.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        explosionSFX = explosion.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            anim.Play("Death");
            PlayerPrefs.SetString("MonsterStatus", "Dead");
            PlayerPrefs.Save();
            StartCoroutine(Wait());
            SceneManager.LoadScene("MonsterBattleResults");
        }
        else if (timesMoved >= 5) { SceneManager.LoadScene("MonsterBattleResults"); }
        slider.value = health;
       x = transform.position.x;
        if (isMoving)
        {
            //move monster towards ship
            if (movingBackward==false && movingForward==true)
            {
               
                transform.Translate(0, 0, moveTowardspeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, height, transform.position.z);
                if(x>=0)
                {
                    movingBackward = false;
                    movingForward = false;
                }
                
            }
            else if (movingForward == false && movingBackward == false)
            {
                if (timesAttacked == timesMoved)
                {
                    if (justHit==false)
                    { anim.Play("Attack1");
                        // ShakeIt(); caused lag
                        //PlayersManager.Opponent1.Health -= 5;
                        PlayerPrefs.SetInt("DamageDoneMonster", PlayerPrefs.GetInt("DamageDoneMonster") + 5);
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        anim.Play("Stand");
                    }
                    anim.CrossFadeQueued("Disappear");
                    //m_Collider.enabled = false;
                    timesAttacked++;
                }
                if ((!anim.IsPlaying("Attack1")) && (!anim.IsPlaying("Disappear")) && !anim.IsPlaying("Stand"))
                {
                    anim.CrossFadeQueued("Appear");
                    m_Collider.enabled = true;
                    transform.position = new Vector3(0, attackHeight, transform.position.z + 5); ;//used to moved monster in front of next cannon
                    justHit = false;
                    anim.CrossFadeQueued("Stand");
                    timesMoved++;
                    movingBackward = true;
                    movingForward = false;
                }

            }
            else if(movingForward==false && movingBackward==true)
            {
                
                
                transform.Translate(0, 0, -moveTowardspeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, height, transform.position.z);
                if (x <= -50)
                {
                    movingBackward = false;
                    movingForward = true;
                }
                
            }
        }
        else
        { anim.Play("Appear");
            anim.Play("Stand");
        }
    }

    void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Cannonball"))
            {

                anim.Play("Hit1");
                health = health - cannonDmg;
                justHit=true;

                explosion.transform.position = transform.position;
                explosionEffect.startSize = 9;
                explosionSFX.Play();
                explosionEffect.Stop();
                explosionEffect.Clear();
                explosionEffect.Play();
        }
            //istriggered = true;

    }
    

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
    }

    /*public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    */
}
