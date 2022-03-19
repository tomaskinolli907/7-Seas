using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour {

    public float multiplier = 1;
    ParticleSystem[] systems;
    List<GameObject> fireList;
    private AudioSource source;
    public AudioClip shipHit;
    public AudioClip pirateVoice1;
    public AudioClip pirateVoice2;
    public bool isHit;
    float shipMove = 0;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        isHit = false;
        fireList = new List<GameObject>();
        systems = GetComponentsInChildren<ParticleSystem>();
        ShipPosition();
    }

    private void OnCollisionEnter(Collision col)
    {
        isHit = true;
        //Debug.Log("hit");
        foreach(ContactPoint contact in col.contacts)
        {
            HitShipExplosion(contact.point);
        }
        StartCoroutine(PlayPirateVoice());
        //source.PlayOneShot(shipHit, 1f);
        //source.PlayOneShot(pirateVoice, 1f);
    }

    public void HitShipExplosion(Vector3 contactPoint)
    {
        foreach (ParticleSystem system in systems)
        {
            var fire = Instantiate(system) as ParticleSystem;
            fire.transform.position = contactPoint;
            ParticleSystem.MainModule mainModule = fire.main;
            mainModule.startSizeMultiplier = multiplier;
            mainModule.startSpeedMultiplier = multiplier;
            mainModule.startLifetimeMultiplier = Mathf.Lerp(multiplier, 1, 0.5f);
            fire.Clear();
            fireList.Add(fire.gameObject);

            fire.Play();
        }
    }

    private void ShipPosition()
    {
        int num = Random.Range(1, 4);
        if (num == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 40f);
            shipMove = .040f;
        }
        if (num == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 60f);
            shipMove = .030f;
        }
        if (num == 3)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 70f);
            shipMove = .020f;
        }
    }

    private void FixedUpdate()
    {
        //if target ship gets hit, start sinking
        /*if (isHit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .01f, transform.position.z);
        }
        //if goes below view, destroy ship
        if(transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }*/
        //move ship along slowly
        transform.position = new Vector3(transform.position.x -shipMove, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        foreach (var item in fireList)
        {
            if (item != null && !item.GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(item);
            }
        }
    }

    IEnumerator PlayPirateVoice()
    {
        source.PlayOneShot(shipHit, 1f);
        source.PlayOneShot(pirateVoice1, 1f);
        yield return new WaitForSeconds(1f);

        source.PlayOneShot(pirateVoice2, 1f);
    }
    

}
