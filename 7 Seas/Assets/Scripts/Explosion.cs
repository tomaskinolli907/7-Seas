using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionTime = 3.0f;

    public static bool impact = false;
    void ExplosionImpact()
    {
        var diceExplosion = GetComponent<ParticleSystem>();
        diceExplosion.Play();
        Destroy(gameObject, explosionTime - 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        var diceExplosion = GetComponent<ParticleSystem>();

        diceExplosion.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (impact)
        {
            ExplosionImpact();
        }
    }
}
