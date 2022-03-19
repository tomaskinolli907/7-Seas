using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // a simple script to scale the size, speed and lifetime of a particle system

        public float multiplier = 1;
        ParticleSystem[] systems;
        List<GameObject> fireList;

        private void Start()
        {
            fireList = new List<GameObject>();
            systems = GetComponentsInChildren<ParticleSystem>();
        }

        public void FireCannon()
        {
           // systemsInstantiate(GetComponentsInChildren<ParticleSystem>()) as  ParticleSystem;
            foreach (ParticleSystem system in systems)
            {
                var fire = Instantiate(system) as ParticleSystem;
                fire.transform.position = system.transform.position;
                ParticleSystem.MainModule mainModule = fire.main;
                mainModule.startSizeMultiplier = multiplier;
                mainModule.startSpeedMultiplier = multiplier;
                mainModule.startLifetimeMultiplier = Mathf.Lerp(multiplier, 1, 0.5f);
                fire.Clear();
                fireList.Add(fire.gameObject);

                fire.Play();
            }
        }


        private void Update()
        {
            foreach(var item in fireList)
            {
                if (item !=null && !item.GetComponent<ParticleSystem>().IsAlive())
                {
                    Destroy(item);
                }
            }
        }


    }
}
