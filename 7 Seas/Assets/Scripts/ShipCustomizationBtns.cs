using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShipCustomizationBtns : MonoBehaviour
{
    // Start is called before the first frame update
    public Button emptyMastBtn;
    public Button mastSelected;
    private static Button recentMastClicked;

    public Button emptyCannonBtn;
    public Button cannonSelected;
    private static Button recentCannonClicked;

    public Button crewBtn;
    public Button treasureBtn;
    public Button damageBtn;

    private static string[] mast = {"mast 1:", "mast 2:", "mast 3:"};
    private static string[] cannon = {"cannon 1:", "cannon 2:", "cannon 3:", "cannon 4:", "cannon 5:"};
    private static string[] crew = {"crew 1:", "crew 2:"};
    private static string[] treasure = {"treasure 1:", "treasure 2:", "treasure 3:"};
    private static string[] damage = { "damage 1:", "damage 2:" };

    private float heightCannonBtn = 30;
    private float widthCannonBtn = 160;
    private static float defaultY = -2.597329f;
    private float defaultX;

    private static bool mastBtnClicked = false;
    private static bool cannonBtnClicked = false;
    private static bool readingPlayerFile = false;

    public Sprite defaultSelect;
    void Start()
    {
        fillSavedChoices();
    }

    public void fillSavedChoices()
    {
        StreamReader rdr = new StreamReader(new FileStream(Application.persistentDataPath + "/Players.txt", FileMode.Open, FileAccess.Read));
        int firstPlayer = 0;
        for (int i = 1; rdr.Peek() > -1; i++)
        {
            if (rdr.ReadLine().Equals("t"))
            {
                firstPlayer = i;
                break;
            }
        }
        Debug.Log(firstPlayer);

        if (firstPlayer > 0)
        {
            readingPlayerFile = true;

            try
            {
                StreamReader read = new StreamReader(new FileStream(Application.persistentDataPath + "/Player" + firstPlayer + ".txt", FileMode.Open, FileAccess.Read));
                while (read.Peek() > -1)
                {
                    string line = read.ReadLine().Trim();
                    if (line.StartsWith("mast "))
                    {
                        if (line.StartsWith("mast 1:"))
                        {
                            if (line.Equals("mast 1: l"))
                            {
                                GameObject.Find("mast1").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                                mast[0] = "mast 1: l";
                                GameObject.Find("mast1").GetComponent<Image>().enabled = true;
                                Debug.Log("mast l");
                            }
                            if (line.Equals("mast 1: s"))
                            {
                                GameObject.Find("mast1").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                                mast[0] = "mast 1: s";
                                GameObject.Find("mast1").GetComponent<Image>().enabled = true;
                                Debug.Log("mast s");
                            }
                        }
                        if (line.StartsWith("mast 2:"))
                        {
                            if (line.Equals("mast 2: l"))
                            {
                                GameObject.Find("mast2").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                                mast[1] = "mast 2: l";
                                GameObject.Find("mast2").GetComponent<Image>().enabled = true;
                                Debug.Log("mast l");
                            }
                            if (line.Equals("mast 2: s"))
                            {
                                GameObject.Find("mast2").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                                mast[1] = "mast 2: s";
                                GameObject.Find("mast2").GetComponent<Image>().enabled = true;
                                Debug.Log("mast s");
                            }
                        }
                        if (line.StartsWith("mast 3:"))
                        {
                            if (line.Equals("mast 3: l"))
                            {
                                GameObject.Find("mast3").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                                mast[2] = "mast 3: l";
                                GameObject.Find("mast3").GetComponent<Image>().enabled = true;
                                Debug.Log("mast l");
                            }
                            if (line.Equals("mast 3: s"))
                            {
                                GameObject.Find("mast3").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                                mast[2] = "mast 3: s";
                                GameObject.Find("mast3").GetComponent<Image>().enabled = true;
                                Debug.Log("mast s");
                            }
                        }
                    }
                    if (line.StartsWith("cannon "))
                    {
                        if (line.StartsWith("cannon 1:"))
                        {
                            if (line.Equals("cannon 1: s"))
                            {
                                GameObject.Find("cannon1").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                                cannon[0] = "cannon 1: s";
                                GameObject.Find("cannon1").GetComponent<Button>().image.enabled = true;
                            }
                            if (line.Equals("cannon 1: l"))
                            {
                                GameObject.Find("cannon1").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                                cannon[0] = "cannon 1: l";
                                GameObject.Find("cannon1").GetComponent<Button>().image.enabled = true;
                            }

                        }
                        if (line.StartsWith("cannon 2:"))
                        {
                            if (line.Equals("cannon 2: s"))
                            {
                                GameObject.Find("cannon2").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                                cannon[1] = "cannon 2: s";
                                GameObject.Find("cannon2").GetComponent<Button>().image.enabled = true;
                            }
                            if (line.Equals("cannon 2: l"))
                            {
                                GameObject.Find("cannon2").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                                cannon[1] = "cannon 2: l";
                                GameObject.Find("cannon2").GetComponent<Button>().image.enabled = true;
                            }
                        }
                        if (line.StartsWith("cannon 3:"))
                        {
                            if (line.Equals("cannon 3: s"))
                            {
                                GameObject.Find("cannon3").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                                cannon[2] = "cannon 3: s";
                                GameObject.Find("cannon3").GetComponent<Button>().image.enabled = true;
                            }
                            if (line.Equals("cannon 3: l"))
                            {
                                GameObject.Find("cannon3").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                                cannon[2] = "cannon 3: l";
                                GameObject.Find("cannon3").GetComponent<Button>().image.enabled = true;
                            }
                        }
                        if (line.StartsWith("cannon 4:"))
                        {
                            if (line.Equals("cannon 4: s"))
                            {
                                GameObject.Find("cannon4").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                                cannon[3] = "cannon 4: s";
                                GameObject.Find("cannon4").GetComponent<Button>().image.enabled = true;
                            }
                            if (line.Equals("cannon 4: l"))
                            {
                                GameObject.Find("cannon4").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                                cannon[3] = "cannon 4: l";
                                GameObject.Find("cannon4").GetComponent<Button>().image.enabled = true;
                            }
                        }
                        if (line.StartsWith("cannon 5:"))
                        {
                            if (line.Equals("cannon 5: s"))
                            {
                                GameObject.Find("cannon5").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                                cannon[4] = "cannon 5: s";
                                GameObject.Find("cannon5").GetComponent<Button>().image.enabled = true;
                            }
                            if (line.Equals("cannon 5: l"))
                            {
                                GameObject.Find("cannon5").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                                cannon[4] = "cannon 5: l";
                                GameObject.Find("cannon5").GetComponent<Button>().image.enabled = true;
                            }
                        }
                    }
                    if(line.StartsWith("crew "))
                    {
                        if(line.StartsWith("crew 1:"))
                        {
                            if (line.Equals("crew 1: t"))
                            {
                                crew[0] = "crew 1: t";
                                GameObject.Find("crew1").GetComponent<Image>().enabled = true;
                            }
                        }
                        if(line.StartsWith("crew 2:"))
                        {
                            if (line.Equals("crew 2: t"))
                            {
                                crew[1] = "crew 2: t";
                                GameObject.Find("crew2").GetComponent<Image>().enabled = true;
                            }
                        }
                    }
                    if (line.StartsWith("treasure "))
                    {
                        if (line.StartsWith("treasure 1:"))
                        {
                            if(line.Equals("treasure 1: t"))
                            {
                                treasure[0] = "treasure 1: t";
                                GameObject.Find("treasure1").GetComponent<Image>().enabled = true;
                            }
                        }
                        if (line.StartsWith("treasure 2:"))
                        {
                            if (line.Equals("treasure 2: t"))
                            {
                                treasure[1] = "treasure 2: t";
                                GameObject.Find("treasure2").GetComponent<Image>().enabled = true;
                            }
                        }
                        if (line.StartsWith("treasure 3:"))
                        {
                            if (line.Equals("treasure 3: t"))
                            {
                                treasure[2] = "treasure 3: t";
                                GameObject.Find("treasure3").GetComponent<Image>().enabled = true;
                            }
                        }
                    }
                    if(line.StartsWith("damage "))
                    {
                        
                        if(line.StartsWith("damage 1:"))
                        {

                            if (line.Equals("damage 1: t"))
                            {
                                damage[0] = "damage 1: t";
                                GameObject.Find("damage1").GetComponent<Image>().enabled = true;
                            }
                        }
                        if(line.StartsWith("damage 2:"))
                        {

                            if (line.Equals("damage 2: t"))
                            {
                                damage[1] = "damage 2: t";
                                GameObject.Find("damage2").GetComponent<Image>().enabled = true;
                            }
                        }
                    }
                    
                }
                read.Close();
            }
            catch (IOException ex)
            {
                readingPlayerFile = true;
            }
        }
    }
    private void resetButtons()
    {
        GameObject.Find("mast1").GetComponent<Image>().sprite = defaultSelect;
       GameObject.Find("mast1").GetComponent<Image>().enabled = false;

        GameObject.Find("mast2").GetComponent<Image>().sprite = defaultSelect;
        GameObject.Find("mast2").GetComponent<Image>().enabled = false;

        GameObject.Find("mast3").GetComponent<Image>().sprite = defaultSelect;
        GameObject.Find("mast3").GetComponent<Image>().enabled = false;
        for(int i = 0; i < 3; i++)
            mast[i] = "mast " + (i + 1) + ":";


        recentMastClicked = null;
        mastBtnClicked = false;


        if (GameObject.Find("cannon1").GetComponent<Button>().GetComponent<Image>().enabled)
        {
            emptyCannonBtn = GameObject.Find("cannon1").GetComponent<Button>();
            cannonClick();
        }
        if (GameObject.Find("cannon2").GetComponent<Button>().GetComponent<Image>().enabled)
        {
            emptyCannonBtn = GameObject.Find("cannon2").GetComponent<Button>();
            cannonClick();
        }
        if (GameObject.Find("cannon3").GetComponent<Button>().GetComponent<Image>().enabled)
        {
            emptyCannonBtn = GameObject.Find("cannon3").GetComponent<Button>();
            cannonClick();
        }
        if (GameObject.Find("cannon4").GetComponent<Button>().GetComponent<Image>().enabled)
        {
            emptyCannonBtn = GameObject.Find("cannon4").GetComponent<Button>();
            cannonClick();
        }
        if (GameObject.Find("cannon5").GetComponent<Button>().GetComponent<Image>().enabled)
        {
            emptyCannonBtn = GameObject.Find("cannon5").GetComponent<Button>();
            cannonClick();
        }

        for (int i = 0; i < 5; i++)
            cannon[i] = "cannon " + (i + 1) + ":";


        if (GameObject.Find("crew1").GetComponent<Button>().image.enabled)
            GameObject.Find("crew1").GetComponent<Button>().image.enabled = false;

        if (GameObject.Find("crew2").GetComponent<Button>().image.enabled)
            GameObject.Find("crew2").GetComponent<Button>().image.enabled = false;

        for (int i = 0; i < 2; i++)
            crew[i] = "crew " + (i + 1) + ":";


        if (GameObject.Find("treasure1").GetComponent<Button>().image.enabled)
            GameObject.Find("treasure1").GetComponent<Button>().image.enabled = false;

        if (GameObject.Find("treasure2").GetComponent<Button>().image.enabled)
            GameObject.Find("treasure2").GetComponent<Button>().image.enabled = false;

        if (GameObject.Find("treasure3").GetComponent<Button>().image.enabled)
            GameObject.Find("treasure3").GetComponent<Button>().image.enabled = false;

        for (int i = 0; i < 3; i++)
            treasure[i] = "treasure " + (i + 1) + ":";


        if (GameObject.Find("damage1").GetComponent<Button>().image.enabled)
            GameObject.Find("damage1").GetComponent<Button>().image.enabled = false;

        if (GameObject.Find("damage2").GetComponent<Button>().image.enabled)
            GameObject.Find("damage2").GetComponent<Button>().image.enabled = false;

        for (int i = 0; i < 2; i++)
            crew[i] = "damage " + (i + 1) + ":";

    }
    
    public void navLeftOrRight()
    {
        StreamReader read = new StreamReader(new FileStream(Application.persistentDataPath + "/Player" + GameObject.Find("shipNum").GetComponent<Text>().text + ".txt", FileMode.Open, FileAccess.Read));

        resetButtons();

        while (read.Peek() > -1)
        {
            string line = read.ReadLine().Trim();
            if (line.StartsWith("mast "))
            {
                if (line.StartsWith("mast 1:"))
                {
                    if (line.Equals("mast 1: l"))
                    {
                        GameObject.Find("mast1").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                        mast[0] = "mast 1: l";
                        GameObject.Find("mast1").GetComponent<Image>().enabled = true;
                        Debug.Log("mast l");
                    }
                    if (line.Equals("mast 1: s"))
                    {
                        GameObject.Find("mast1").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                        mast[0] = "mast 1: s";
                        GameObject.Find("mast1").GetComponent<Image>().enabled = true;
                        Debug.Log("mast s");
                    }
                }
                if (line.StartsWith("mast 2:"))
                {
                    if (line.Equals("mast 2: l"))
                    {
                        GameObject.Find("mast2").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                        mast[1] = "mast 2: l";
                        GameObject.Find("mast2").GetComponent<Image>().enabled = true;
                        Debug.Log("mast l");
                    }
                    if (line.Equals("mast 2: s"))
                    {
                        GameObject.Find("mast2").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                        mast[1] = "mast 2: s";
                        GameObject.Find("mast2").GetComponent<Image>().enabled = true;
                        Debug.Log("mast s");
                    }
                }
                if (line.StartsWith("mast 3:"))
                {
                    if (line.Equals("mast 3: l"))
                    {
                        GameObject.Find("mast3").GetComponent<Image>().sprite = GameObject.Find("LargeMast").GetComponent<Image>().sprite;
                        mast[2] = "mast 3: l";
                        GameObject.Find("mast3").GetComponent<Image>().enabled = true;
                        Debug.Log("mast l");
                    }
                    if (line.Equals("mast 3: s"))
                    {
                        GameObject.Find("mast3").GetComponent<Image>().sprite = GameObject.Find("ShortMast").GetComponent<Image>().sprite;
                        mast[2] = "mast 3: s";
                        GameObject.Find("mast3").GetComponent<Image>().enabled = true;
                        Debug.Log("mast s");
                    }
                }
            }
            if (line.StartsWith("cannon "))
            {
                if (line.StartsWith("cannon 1:"))
                {
                    if (line.Equals("cannon 1: s"))
                    {
                        GameObject.Find("cannon1").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                        cannon[0] = "cannon 1: s";
                        GameObject.Find("cannon1").GetComponent<Button>().image.enabled = true;
                    }
                    if (line.Equals("cannon 1: l"))
                    {
                        GameObject.Find("cannon1").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                        cannon[0] = "cannon 1: l";
                        GameObject.Find("cannon1").GetComponent<Button>().image.enabled = true;
                    }

                }
                if (line.StartsWith("cannon 2:"))
                {
                    if (line.Equals("cannon 2: s"))
                    {
                        GameObject.Find("cannon2").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                        cannon[1] = "cannon 2: s";
                        GameObject.Find("cannon2").GetComponent<Button>().image.enabled = true;
                    }
                    if (line.Equals("cannon 2: l"))
                    {
                        GameObject.Find("cannon2").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                        cannon[1] = "cannon 2: l";
                        GameObject.Find("cannon2").GetComponent<Button>().image.enabled = true;
                    }
                }
                if (line.StartsWith("cannon 3:"))
                {
                    if (line.Equals("cannon 3: s"))
                    {
                        GameObject.Find("cannon3").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                        cannon[2] = "cannon 3: s";
                        GameObject.Find("cannon3").GetComponent<Button>().image.enabled = true;
                    }
                    if (line.Equals("cannon 3: l"))
                    {
                        GameObject.Find("cannon3").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                        cannon[2] = "cannon 3: l";
                        GameObject.Find("cannon3").GetComponent<Button>().image.enabled = true;
                    }
                }
                if (line.StartsWith("cannon 4:"))
                {
                    if (line.Equals("cannon 4: s"))
                    {
                        GameObject.Find("cannon4").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                        cannon[3] = "cannon 4: s";
                        GameObject.Find("cannon4").GetComponent<Button>().image.enabled = true;
                    }
                    if (line.Equals("cannon 4: l"))
                    {
                        GameObject.Find("cannon4").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                        cannon[3] = "cannon 4: l";
                        GameObject.Find("cannon4").GetComponent<Button>().image.enabled = true;
                    }
                }
                if (line.StartsWith("cannon 5:"))
                {
                    if (line.Equals("cannon 5: s"))
                    {
                        GameObject.Find("cannon5").GetComponent<Image>().sprite = GameObject.Find("ShortCannon").GetComponent<Image>().sprite;
                        cannon[4] = "cannon 5: s";
                        GameObject.Find("cannon5").GetComponent<Button>().image.enabled = true;
                    }
                    if (line.Equals("cannon 5: l"))
                    {
                        GameObject.Find("cannon5").GetComponent<Image>().sprite = GameObject.Find("LongCannon").GetComponent<Image>().sprite;
                        cannon[4] = "cannon 5: l";
                        GameObject.Find("cannon5").GetComponent<Button>().image.enabled = true;
                    }
                }
            }
            if (line.StartsWith("crew "))
            {
                if (line.StartsWith("crew 1:"))
                {
                    if (line.Equals("crew 1: t"))
                    {
                        crew[0] = "crew 1: t";
                        GameObject.Find("crew1").GetComponent<Image>().enabled = true;
                    }
                }
                if (line.StartsWith("crew 2:"))
                {
                    if (line.Equals("crew 2: t"))
                    {
                        crew[1] = "crew 2: t";
                        GameObject.Find("crew2").GetComponent<Image>().enabled = true;
                    }
                }
            }
            if (line.StartsWith("treasure "))
            {
                if (line.StartsWith("treasure 1:"))
                {
                    if (line.Equals("treasure 1: t"))
                    {
                        treasure[0] = "treasure 1: t";
                        GameObject.Find("treasure1").GetComponent<Image>().enabled = true;
                    }
                }
                if (line.StartsWith("treasure 2:"))
                {
                    if (line.Equals("treasure 2: t"))
                    {
                        treasure[1] = "treasure 2: t";
                        GameObject.Find("treasure2").GetComponent<Image>().enabled = true;
                    }
                }
                if (line.StartsWith("treasure 3:"))
                {
                    if (line.Equals("treasure 3: t"))
                    {
                        treasure[2] = "treasure 3: t";
                        GameObject.Find("treasure3").GetComponent<Image>().enabled = true;
                    }
                }
            }
            if (line.StartsWith("damage "))
            {

                if (line.StartsWith("damage 1:"))
                {

                    if (line.Equals("damage 1: t"))
                    {
                        damage[0] = "damage 1: t";
                        GameObject.Find("damage1").GetComponent<Image>().enabled = true;
                    }
                }
                if (line.StartsWith("damage 2:"))
                {

                    if (line.Equals("damage 2: t"))
                    {
                        damage[1] = "damage 2: t";
                        GameObject.Find("damage2").GetComponent<Image>().enabled = true;
                    }
                }
            }

        }
        read.Close();
    }

    private void Update()
    {
        //Debug.Log(string.Join(", ", mast)+"                  "+string.Join(", ",cannon));
    }

    public void mastClick()
    {
        if (emptyMastBtn.image.enabled)
        {
            if (emptyMastBtn.name.Equals("mast1"))
                mast[0] = "mast 1:";
            if (emptyMastBtn.name.Equals("mast2"))
                mast[1] = "mast 2:";
            if (emptyMastBtn.name.Equals("mast3"))
                mast[2] = "mast 3:";

            emptyMastBtn.image.enabled = false;
            recentMastClicked = null;
            mastBtnClicked = false;
        }
        else if (!mastBtnClicked)
        {
            emptyMastBtn.image.sprite = defaultSelect;
            emptyMastBtn.image.enabled = true;
            recentMastClicked = emptyMastBtn;
            mastBtnClicked = true;
        }
    }
    public void mastSelection()
    {
        if (recentMastClicked != null)
        {
            if (recentMastClicked.name.Equals("mast1"))
            {
                if (mastSelected.name.Equals("ShortMast"))
                {
                    mast[0] = "mast 1: s";
                    recentMastClicked.GetComponent<Image>().sprite = mastSelected.GetComponent<Image>().sprite;
                }
                else
                {
                    mast[0] = "mast 1: l";
                }
            }
            if (recentMastClicked.name.Equals("mast2"))
            {
                if (mastSelected.name.Equals("ShortMast"))
                {
                    mast[1] = "mast 2: s";
                }
                else
                {
                    mast[1] = "mast 2: l";
                }
            }
            if (recentMastClicked.name.Equals("mast3"))
            {
                if (mastSelected.name.Equals("ShortMast"))
                {
                    mast[2] = "mast 3: s";
                }
                else
                {
                    mast[2] = "mast 3: l";
                }
            }
            recentMastClicked.GetComponent<Image>().sprite = mastSelected.GetComponent<Image>().sprite;
            mastBtnClicked = false;
        }
    }

    
    public void cannonClick()
    {
        if (emptyCannonBtn.image.enabled)
        {
            if (emptyCannonBtn.name.Equals("cannon1"))
                cannon[0] = "cannon 1:";
            if (emptyCannonBtn.name.Equals("cannon2"))
                cannon[1] = "cannon 2:";
            if (emptyCannonBtn.name.Equals("cannon3"))
                cannon[2] = "cannon 3:";
            if (emptyCannonBtn.name.Equals("cannon4"))
                cannon[3] = "cannon 4:";
            if (emptyCannonBtn.name.Equals("cannon5"))
                cannon[4] = "cannon 5:";

            emptyCannonBtn.image.enabled = false;
            recentCannonClicked = null;
            cannonBtnClicked = false;
        }
        else if(!cannonBtnClicked)
        {
            emptyCannonBtn.image.sprite = defaultSelect;
            emptyCannonBtn.image.enabled = true;
            recentCannonClicked = emptyCannonBtn;
            cannonBtnClicked = true;
        }
    }
    
    public void cannonSelection()
    {
        if(recentCannonClicked != null)
        {
            if (recentCannonClicked.name.Equals("cannon1"))
            {
                if (cannonSelected.name.Equals("ShortCannon"))
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn*2.1f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.03f, recentCannonClicked.GetComponent<RectTransform>().position.y-0.45f, recentCannonClicked.GetComponent<RectTransform>().position.z); 
                    cannon[0] = "cannon 1: s";
                }
                else
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn*2.54f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.01f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.65f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[0] = "cannon 1: l";
                }
            }
            if (recentCannonClicked.name.Equals("cannon2"))
            {
                if (cannonSelected.name.Equals("ShortCannon"))
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.1f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.03f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.45f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[1] = "cannon 2: s";
                }
                else
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.54f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.01f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.65f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[1] = "cannon 2: l";
                }
            }
            if (recentCannonClicked.name.Equals("cannon3"))
            {
                if (cannonSelected.name.Equals("ShortCannon"))
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.1f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.03f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.45f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[2] = "cannon 3: s";
                }
                else
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.54f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.01f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.65f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[2] = "cannon 3: l";
                }
            }
            if (recentCannonClicked.name.Equals("cannon4"))
            {
                if (cannonSelected.name.Equals("ShortCannon"))
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.1f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.03f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.45f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[3] = "cannon 4: s";
                }
                else
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.54f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.01f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.65f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[3] = "cannon 4: l";
                }
            }
            if (recentCannonClicked.name.Equals("cannon5"))
            {
                if (cannonSelected.name.Equals("ShortCannon"))
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.1f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.03f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.45f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[4] = "cannon 5: s";
                }
                else
                {
                    //recentCannonClicked.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightCannonBtn * 2.54f);
                    //recentCannonClicked.GetComponent<RectTransform>().position = new Vector3(recentCannonClicked.GetComponent<RectTransform>().position.x-0.01f, recentCannonClicked.GetComponent<RectTransform>().position.y - 0.65f, recentCannonClicked.GetComponent<RectTransform>().position.z);
                    cannon[4] = "cannon 5: l";
                }
            }
            recentCannonClicked.GetComponent<Image>().sprite = cannonSelected.GetComponent<Image>().sprite;
            cannonBtnClicked = false;
        }
    }

    public void crewSelect()
    {
        if (crewBtn.image.enabled)
        {
            if (crewBtn.name.Equals("crew1"))
                crew[0] = "crew 1:";
            if (crewBtn.name.Equals("crew2"))
                crew[1] = "crew 2:";
            crewBtn.image.enabled = false;
        }
        else
        {
            if (crewBtn.name.Equals("crew1"))
                crew[0] = "crew 1: t";
            if (crewBtn.name.Equals("crew2"))
                crew[1] = "crew 2: t";
            crewBtn.image.enabled = true;
        }
    }
    public void treasureSelect()
    {
        if (treasureBtn.image.enabled)
        {
            if (treasureBtn.name.Equals("treasure1"))
                treasure[0] = "treasure 1:";
            if (treasureBtn.name.Equals("treasure2"))
                treasure[1] = "treasure 2:";
            if (treasureBtn.name.Equals("treasure3"))
                treasure[2] = "treasure 3:";
            treasureBtn.image.enabled = false;
        }
        else
        {
            if (treasureBtn.name.Equals("treasure1"))
                treasure[0] = "treasure 1: t";
            if (treasureBtn.name.Equals("treasure2"))
                treasure[1] = "treasure 2: t";
            if (treasureBtn.name.Equals("treasure3"))
                treasure[2] = "treasure 3: t";
            treasureBtn.image.enabled = true;
        }
    }

    public void damageSelect()
    {
        if (damageBtn.image.enabled)
        {
            if (damageBtn.name.Equals("damage1"))
                damage[0] = "damage 1:";
            if (damageBtn.name.Equals("damage2"))
                damage[1] = "damage 2:";
            damageBtn.image.enabled = false;
        }
        else
        {
            if (damageBtn.name.Equals("damage1"))
                damage[0] = "damage 1: t";
            if (damageBtn.name.Equals("damage2"))
                damage[1] = "damage 2: t";
            damageBtn.image.enabled = true;
        }

    }
    public void save()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Player"+ GameObject.Find("shipNum").GetComponent<Text>().text+".txt", string.Join("\n", mast)+"\n"+
                                                                                                                                             string.Join("\n", cannon)+"\n"+
                                                                                                                                             string.Join("\n", crew)+"\n"+
                                                                                                                                             string.Join("\n", treasure)+"\n"+
                                                                                                                                             string.Join("\n", damage));
    }
}
