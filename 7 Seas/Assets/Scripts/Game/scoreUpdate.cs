﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreUpdate : MonoBehaviour
{
    public Text MyText;
    public int score;
    int highestScore;
    public PointsManager manager;
    private bool hit;
    private GameObject explosion;
    private ParticleSystem explosionEffect;
    private AudioSource explosionSFX;
    


    // Start is called before the first frame update
    void Start()
    {
        
        score = 0;

        manager = GetComponent<PointsManager>();
        manager = FindObjectOfType<PointsManager>();
        this.hit = false;

        explosion = GameObject.Find("ScoreExplosion");
        explosionEffect = explosion.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        explosionSFX = explosion.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
  


    void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetString("Enemy").Equals("Player") && other.gameObject.layer == 9) // Target layer is 9
        {
            ShipCombatTarget teleporter = other.gameObject.GetComponentInParent<ShipCombatTarget>();
            teleporter.MoveTargetToRandomPosition();
        }
        if (PlayerPrefs.GetString("Enemy").Equals("Treasure") && other.gameObject.layer == 9)
        {
            ShipCombatTarget teleporter = other.gameObject.GetComponentInParent<ShipCombatTarget>();
            
        }
        if (other.CompareTag("+1") && (this.hit == false))
        {
            explosion.transform.position = transform.position;
            explosionEffect.startSize = 1;
            explosionSFX.Play();
            explosionEffect.Stop();
            explosionEffect.Clear();
            explosionEffect.Play(); 
            this.hit = true;
            score = score + 1;
            manager.AddPoints(score);
            Debug.Log(score + " hit registered");
                     
        }
        else if (other.CompareTag("+2") && (this.hit == false))
        {
            explosion.transform.position = transform.position;
            explosionEffect.startSize = 3;
            explosionSFX.Play();
            explosionEffect.Stop();
            explosionEffect.Clear();
            explosionEffect.Play();
            this.hit = true;
            score = score + 2;
            manager.AddPoints(score);
            Debug.Log(score + " hit registered");
          
            
        }
        else if (other.CompareTag("+3") && (this.hit == false))
        {
            explosion.transform.position = transform.position;
            explosionEffect.startSize = 9;
            explosionSFX.Play();
            explosionEffect.Stop();
            explosionEffect.Clear();
            explosionEffect.Play();
            this.hit = true;
            score = score + 3;
            manager.AddPoints(score);
            Debug.Log(score + " hit registered");
           
            
        }
        else if (other.CompareTag("+4") && (this.hit == false))
        {
            explosion.transform.position = transform.position;
            explosionEffect.startSize = 15;
            explosionSFX.Play();
            explosionEffect.Stop();
            explosionEffect.Clear();
            explosionEffect.Play();
            this.hit = true;
            score = score + 4;
            manager.AddPoints(score);
            Debug.Log(score + " hit registered");
            
            
        }
        else { Debug.Log(" No Tag"); }
        PlayerPrefs.Save();
    }

   
}
