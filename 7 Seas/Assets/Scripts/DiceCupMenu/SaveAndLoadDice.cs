using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


public class SaveAndLoadDice : MonoBehaviour {

    public DiceCupMain diceCupMain;
    public GameObject mainLoop;
    public string cupSelected;
    public RestoreDefault restoreDefault;


    // Use this for initialization
    void Start () {

        diceCupMain = mainLoop.GetComponent<DiceCupMain>();

        //determine which cup is selected
        SelectedCup();

        //load initial values
        DiceCupMain.loaded = true;
        //DiceCupMain.moveNumDice.Clear();

        //load the saves or the default
        LoadSavesOrDefault();
        Load();


    }



    //check for save files if they exist load them at start, if not then load defaults
    public void LoadSavesOrDefault()
    {
        if (ES2.Exists(cupSelected + "movementNumDice"))
        {
            DiceCupMain.moveNumDice = ES2.LoadList<Sprite>(cupSelected + "movementNumDice");
        }
        else
        {
            
            Debug.Log("file not found!!!");
        }

        DiceCupMain.tarShipDice.Clear();
        if (ES2.Exists(cupSelected + "targetShipDice"))
        {
            DiceCupMain.tarShipDice = ES2.LoadList<Sprite>(cupSelected + "targetShipDice");
        }
        else
        {
            //DiceCupMain.tarShipDice = diceCupMain.swabieTarShipDice;
            Debug.Log("file not found!!!");
        }

        DiceCupMain.windMovDice.Clear();
        if (ES2.Exists(cupSelected + "windMovementDice"))
        {
            DiceCupMain.windMovDice = ES2.LoadList<Sprite>(cupSelected + "windMovementDice");
        }
        else
        {
            Debug.Log("file not found!!!");
        }

        DiceCupMain.resourceDice.Clear();
        if (ES2.Exists(cupSelected + "resourceDice"))
        {
            DiceCupMain.resourceDice = ES2.LoadList<Sprite>(cupSelected + "resourceDice");
        }
        else
        {
            Debug.Log("file not found!!!");
        }

        DiceCupMain.colorDice.Clear();
        if (ES2.Exists(cupSelected + "colorDice"))
        {
            DiceCupMain.colorDice = ES2.LoadList<Sprite>(cupSelected + "colorDice");
        }
        else
        {
            Debug.Log("file not found!!!");
            restoreDefault.SetDefault();
        }
    }


    //determines which cup is selected
    public void SelectedCup()
    {
        cupSelected = "";
        cupSelected = PlayerPrefs.GetString("Difficulty");

    } 

    public void Save()
    {


        diceCupMain.LoadTarShipArray();
        diceCupMain.LoadNumMovArray();
        diceCupMain.LoadWindMovArray();
        diceCupMain.LoadResourceArray();
        diceCupMain.LoadColorArray();
        //determine which cup is selected
        SelectedCup();

        //save each dice array to the selected cup's save files
        ES2.Save(DiceCupMain.tarShipDice, cupSelected + "targetShipDice");
        ES2.Save(DiceCupMain.moveNumDice, cupSelected + "movementNumDice");
        ES2.Save(DiceCupMain.windMovDice, cupSelected + "windMovementDice");
        ES2.Save(DiceCupMain.resourceDice, cupSelected + "resourceDice");
        ES2.Save(DiceCupMain.colorDice, cupSelected + "colorDice");

        diceCupMain.MatchTargetShipGold();
        ES2.Save(diceCupMain.tarShipGold, cupSelected + "tarShipGold");

    }

    public void Load()
    {
        //determine which cup is selected
        SelectedCup();

        //load each dice array from the selected cup's save files
        DiceCupMain.loaded = true;
        DiceCupMain.moveNumDice.Clear();
        DiceCupMain.moveNumDice = ES2.LoadList<Sprite>(cupSelected + "movementNumDice");
        DiceCupMain.tarShipDice.Clear();
        DiceCupMain.tarShipDice = ES2.LoadList<Sprite>(cupSelected + "targetShipDice");
        DiceCupMain.windMovDice.Clear();
        DiceCupMain.windMovDice = ES2.LoadList<Sprite>(cupSelected + "windMovementDice");
        DiceCupMain.resourceDice.Clear();
        DiceCupMain.resourceDice = ES2.LoadList<Sprite>(cupSelected + "resourceDice");
        DiceCupMain.colorDice.Clear();
        DiceCupMain.colorDice = ES2.LoadList<Sprite>(cupSelected + "colorDice");

        diceCupMain.tarShipGold.Clear();
        diceCupMain.tarShipGold = ES2.LoadList<int>(cupSelected + "tarShipGold");

        //load dice numbers
        LoadTarShipGold();
        LoadDiceNumbers("NumMovDice", DiceCupMain.moveNumDice);
        LoadDiceNumbers("WindMovDice", DiceCupMain.windMovDice);
        LoadDiceNumbers("ResourceDice", DiceCupMain.resourceDice);
        LoadDiceNumbers("ColorDice", DiceCupMain.colorDice);

    }

    //get all the varying target shit gold amounts from the dice and load them in
    public void LoadTarShipGold()
    {
        var itemsList = GameObject.FindGameObjectsWithTag("TargetShip");

        foreach (var item in itemsList)
        {
            if (item.GetComponentInChildren<Text>().name == "FranceText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[0].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "EnglandText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[1].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "HollandText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[2].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "SpainText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[3].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "ItalyText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[4].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "PortugalText")
            {
                item.GetComponentInChildren<Text>().text = diceCupMain.tarShipGold[5].ToString();
            }
            else if (item.GetComponentInChildren<Text>().name == "tarShipGhost")
            {
                int num = 0;
                var images = item.GetComponentsInChildren<Image>();
                foreach (var item2 in DiceCupMain.tarShipDice)
                {
                    if (item2.name == images[5].sprite.name)
                    {
                        //text number
                        ++num;
                    }
                }
                item.GetComponentInChildren<Text>().text = num.ToString();
            }
        }
    }

    public void LoadDiceNumbers(string tag, List<Sprite> spriteList)
    {
        //load dice numbers
        var itemsList = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in itemsList)
        {
            int num = 0;
            var images = item.GetComponentsInChildren<Image>();
            foreach (var item2 in spriteList)
            {
                if (item2.name == images[5].sprite.name)
                {
                    //text number
                    ++num;
                }
                
            }
            item.GetComponentInChildren<Text>().text = num.ToString();

        }

    }

    //for play game button
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

}
