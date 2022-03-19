using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FCP
{
    public class Cannon : MonoBehaviour
    {
        public GameObject CI;
        GameObject cannon;
        public GameObject cannon1;
        public GameObject cannon2;
        public GameObject cannon3;
        public Slider horizSlider;
        public Slider vertSlider;
        float vertSlider1 = 0;
        float vertSlider2 = 0;
        float vertSlider3 = 0;
        float horizSlider1 = 0;
        float horizSlider2 = 0;
        float horizSlider3 = 0;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        public float horizTurnDegree;
        public float vertTurnDegree;
        GameObject cannonBall;
        public GameObject cannonBall1;
        public GameObject cannonBall2;
        public GameObject cannonBall3;
        public List<GameObject> projectileList;
        public GameObject explosion;
        private AudioSource source;
        public AudioClip cannonFire;
        public AudioClip movingSound;
        public AudioClip[] pirateSounds;
        //public GameObject cannonBarrel;
        float oldZ;
        float oldX;
        public float recoilAmount = .2f;
        //public ParticleSystem[] explosionParticles;

        // Use this for initialization
        void Start()
        {
            //oldX = cannonBarrel.transform.position.x;
            //oldZ = cannonBarrel.transform.position.z;
            source = GetComponent<AudioSource>();
            //explosionParticles = explosion.GetComponentsInChildren<ParticleSystem>();
            projectileList = new List<GameObject>();
            horizTurnDegree = 10.0f;
            vertTurnDegree = 7.0f;
            //canonBall.GetComponent<Rigidbody>().

            //cannon.transform.rotation = Quaternion.identity;
            //cannon.transform.Rotate(0, 0, 0);

            cannonBall = new GameObject();

            cannon1.transform.rotation = Quaternion.identity;
            cannon1.transform.Rotate(-92, 92, 0);
            cannon2.transform.rotation = Quaternion.identity;
            cannon2.transform.Rotate(-92, 92, 0);
            cannon3.transform.rotation = Quaternion.identity;
            cannon3.transform.Rotate(-92, 92, 0);

            QualitySettings.SetQualityLevel(2);
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 70;

            PlayerPrefs.SetString("cannonVisited", "true");

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                CI.GetComponent<CannonInventory>().kegs += 10;
                CI.GetComponent<CannonInventory>().Cannon += 10;
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
                QualitySettings.SetQualityLevel(0);

            else if (Input.GetKeyDown(KeyCode.Alpha1))
                QualitySettings.SetQualityLevel(1);

            else if (Input.GetKeyDown(KeyCode.Alpha2))
                QualitySettings.SetQualityLevel(2);

            else if (Input.GetKeyDown(KeyCode.Alpha3))
                QualitySettings.SetQualityLevel(3);

            else if (Input.GetKeyDown(KeyCode.Alpha4))
                QualitySettings.SetQualityLevel(4);

            else if (Input.GetKeyDown(KeyCode.Alpha5))
                QualitySettings.SetQualityLevel(5);


            if (cannon1.GetComponent<Outline>().enabled)
            {
                flag2 = false;
                flag3 = false;
                if (!flag1)
                {
                    vertSlider.value = vertSlider1;
                    horizSlider.value = horizSlider1;
                    cannonBall = cannonBall1;
                    cannon = cannon1;
                    flag1 = true;
                }
                cannon1.transform.rotation = Quaternion.identity;
                cannon1.transform.Rotate(-92, 92, 0);
                cannon1.transform.Rotate(0, vertSlider.value * vertTurnDegree, horizSlider.value * horizTurnDegree);
                vertSlider1 = vertSlider.value;
                horizSlider1 = horizSlider.value;
            }
            else if (cannon2.GetComponent<Outline>().enabled)
            {
                flag1 = false;
                flag3 = false;
                if (!flag2)
                {
                    vertSlider.value = vertSlider2;
                    horizSlider.value = horizSlider2;
                    cannonBall = cannonBall2;
                    cannon = cannon2;
                    flag2 = true;
                }
                cannon2.transform.rotation = Quaternion.identity;
                cannon2.transform.Rotate(-92, 92, 0);
                cannon2.transform.Rotate(0, vertSlider.value * vertTurnDegree, horizSlider.value * horizTurnDegree);
                vertSlider2 = vertSlider.value;
                horizSlider2 = horizSlider.value;
            }
            else if (cannon3.GetComponent<Outline>().enabled)
            {
                flag1 = false;
                flag2 = false;
                if (!flag3)
                {
                    vertSlider.value = vertSlider3;
                    horizSlider.value = horizSlider3;
                    cannonBall = cannonBall3;
                    cannon = cannon3;
                    flag3 = true;
                }
                cannon3.transform.rotation = Quaternion.identity;
                cannon3.transform.Rotate(-92, 92, 0);
                cannon3.transform.Rotate(0, vertSlider.value * vertTurnDegree, horizSlider.value * horizTurnDegree);
                vertSlider3 = vertSlider.value;
                horizSlider3 = horizSlider.value;
            }

            else
            {
                vertSlider.value = 0;
                horizSlider.value = 0;
                flag1 = false;
                flag2 = false;
                flag3 = false;
                cannonBall = null;
            }

            foreach (var item in projectileList)
            {
                if (item != null && item.transform.position.y < -40)
                {
                    Destroy(item);
                }
            }

            /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Vector3 pos = cannonBall.transform.position;
                GameObject projectile = Instantiate(cannonBall, pos, Quaternion.identity) as GameObject;
                projectile.AddComponent<Rigidbody>();
                projectile.GetComponent<Rigidbody>().useGravity = true;
                projectile.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * 1500);
            }*/

        }

        public void FireCannon()
        {
            int kegAmt = CI.GetComponent<CannonInventory>().kegs;
            int cbAmt = CI.GetComponent<CannonInventory>().Cannon;
            //if we have at least 1 keg and 1 cb
            if (kegAmt > 0 && cbAmt > 0)
                //and if at least one of the cannons are selected
                if (flag1 || flag2 || flag3)
                {
                    {
                        // var keg = CI.GetComponent<CannonInventory>().kegs;
                        CI.GetComponent<CannonInventory>().CheckAmmo();
                        CI.GetComponent<CannonInventory>().kegs -= 1;
                        CI.GetComponent<CannonInventory>().Cannon -= 1;
                        CI.GetComponent<CannonInventory>().kegamount.text = "Kegs: " + CI.GetComponent<CannonInventory>().kegs.ToString();
                        CI.GetComponent<CannonInventory>().CannonBallAmount.text = "CBs: " + CI.GetComponent<CannonInventory>().Cannon.ToString();
                        //cannonBarrel.GetComponent<Animator>().Play("CannonRecoil");
                        //cannonBarrel.GetComponent<Animation>().Play();

                        /*cannonBarrel.transform.position = new Vector3(cannonBarrel.transform.position.x,
                            cannonBarrel.transform.position.y, cannonBarrel.transform.position.z + 1f);*/

                        source.PlayOneShot(pirateSounds[(int)Random.Range(0, 6)], 1f);
                        source.PlayOneShot(cannonFire, 1f);
                        Vector3 pos = cannonBall.transform.position;
                        GameObject projectile = Instantiate(cannonBall, pos, Quaternion.identity) as GameObject;
                        projectile.SetActive(true);
                        projectileList.Add(projectile);

                        projectile.transform.rotation = Quaternion.identity;
                        projectile.transform.Rotate(0, 0, 0);
                        //projectile.transform.localPosition 

                        //projectile.transform.localScale = new Vector3(.4f, .4f, .4f);

                       projectile.AddComponent<Rigidbody>();

                        //projectile.AddComponent<Rigidbody>().mass = 10;
                        //projectile.GetComponents<Rigidbody>().mas;

                        projectile.GetComponent<Rigidbody>().useGravity = true;
                        projectile.GetComponent<Rigidbody>().AddForce(-cannon.transform.right * 2500);
                        projectile.GetComponent<SphereCollider>().enabled = true;

                        //Debug.Log(projectile.transform.localScale);

                        cannon.GetComponentInChildren<UnityStandardAssets.Effects.ParticleSystemMultiplier>().FireCannon();

                        /*cannonBarrel.transform.position = new Vector3(cannonBarrel.transform.position.x - cannonBarrel.transform.rotation.y * recoilAmount,
                            cannonBarrel.transform.position.y, cannonBarrel.transform.position.z - recoilAmount);*/
                    }

                }
        }

        /*private void FixedUpdate()
        {
            cannonBarrel.transform.position = Vector3.Lerp(cannonBarrel.transform.position,
                new Vector3(oldX, cannonBarrel.transform.position.y, oldZ), Time.deltaTime * 20f);
        }*/

        public void MovingCannonSound()
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(movingSound, 1f);
            }
        }


    }
}
