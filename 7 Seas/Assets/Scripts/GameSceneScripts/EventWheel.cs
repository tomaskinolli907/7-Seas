using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventWheel : MonoBehaviour
{

    public GameObject pirateWheel;
    public GameObject roundEvents;
    public GameObject evSpinButton;
    public GameLoop gameLoop;
    public GameObject endTurnButton;
    public GameObject volcano;
    public Volcano myVolcano;
    public LoseTurn loseTurn;
    public bool toggleActive;
    public bool toggleRotate;
    public bool textgen;
    bool eventChange;
    public float rotZ;
    public Text PirateText;
    public Text currentEvent;
    string [] EventsArray = new string [11];
    int randnum = 0;
    float timetotal;
    float temptime = .1f;
    private AudioSource source;
    public AudioClip clip;

    void Start()
    {
        EventsArray[0] = "Volcanoes Get Lava";
        EventsArray[1] = "Lose Turn Shoal";
        EventsArray[2] = "Lose Turn Ocean";
        EventsArray[3] = "Lose Turn Deep Blue";
        EventsArray[4] = "Sirens on Reef";
        EventsArray[5] = "Storm Wind 2";
        EventsArray[6] = "Double Gold";
        EventsArray[7] = "Leviathan";
        EventsArray[8] = "Fog";
        EventsArray[9] = "Add Rats";
        EventsArray[10] = "Extra Ghost Ship";


        rotZ = 720;
        toggleActive = true;
        toggleRotate = false;
        textgen = false;
        eventChange = false;
        

        PirateText = GameObject.Find("pirateText").GetComponent<Text>();
        currentEvent = GameObject.Find("CurrentEvent").GetComponent<Text>();
        PirateText.text = "";
        currentEvent.text = "";
        endTurnButton.SetActive(false);

        myVolcano = volcano.GetComponent<Volcano>();

        source = GetComponent<AudioSource>();

    }

    //check if we can rotate the wheel and if so, rotate it
    void FixedUpdate()
    {

        if (toggleRotate == true)
        {
            textgen = true;
            rotZ = Mathf.Lerp(rotZ, 0, Time.deltaTime);
            pirateWheel.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            if (textgen == true)
            {
                timetotal += Time.deltaTime;
                if (timetotal >= temptime)
                {
                    temptime += .03f;
                    textgenerator();
                    timetotal = 0;
                }
            }
           
            if (rotZ < 10)
            {
                textgen = false;
                if(eventChange == true)
                {
                    roundEvents.GetComponent<RoundEvents>().changeEvent();
                    eventChange = false;
                    volcano.GetComponent<Volcano>().addroundlava();
                }

                PirateText.text = "Event: " + roundEvents.GetComponent<RoundEvents>().currentEvent;
                currentEvent.text = "Event: " + roundEvents.GetComponent<RoundEvents>().currentEvent;
                currentEvent.enabled = true;

                if (rotZ < 4.0f)
                {
                    endTurnButton.SetActive(true);
                    pirateWheel.SetActive(false);
                    evSpinButton.SetActive(false);
                    toggleActive = false;
                    toggleRotate = false;
                    textgen = false;
                    rotZ = Random.Range(740, 1040);
                    PirateText.text = "";
                    loseTurn.CheckTurnLoss();

                }
            }
        }
    }

    //reset the event wheel stuff when done with it
    public void ResetEventWheel()
    {
       if (gameLoop.playersTurn == 1)
        {
            roundEvents.GetComponent<RoundEvents>().currentEvent = null;
            endTurnButton.SetActive(false);
            pirateWheel.SetActive(true);
            pirateWheel.transform.rotation = Quaternion.Euler(0, 0, 0);
            evSpinButton.SetActive(true);
            temptime = .1f;
            currentEvent.enabled = false;
            eventChange = false;
            evSpinButton.SetActive(true);
        }
    }


    //spin wheel booleans
    public void WheelSpin()
    {
        toggleRotate = true;
        eventChange = true;
        evSpinButton.SetActive(false);
        PlayerPrefs.SetString("wheelSpun", "true");
    }

    //displays the events to the screen during the wheel spin
    public void textgenerator()
    {
        randnum = Random.Range(0, 11);
        PirateText.text = "Event: " + EventsArray[randnum];
        source.PlayOneShot(clip);
    }

}
