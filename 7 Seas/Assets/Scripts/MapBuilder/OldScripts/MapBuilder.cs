using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBuilder : MonoBehaviour
{
    public Vector2 MapSize;
    public GameObject[,] tileMap;
    public GameObject canvas;
    public GameObject button;
    GameObject newCanvas;
    public float tileSize;
    public Sprite sprite;
    float horizExtent;
    string text;
    public List<Sprite> tileList;
    bool fullSize = false;
    public Text inputField;
    public string fileName = "";
    public GameObject saveMenu;
    public GameObject deleteMenu;

    // Use this for initialization
    void Start()
    {
        saveMenu.SetActive(false);
        deleteMenu.SetActive(false);
        //initialize filename to any previous saved file name
        if (PlayerPrefs.HasKey("customMapName"))
        {
            fileName = PlayerPrefs.GetString("customMapName");
        }

        tileSize = 37;
        MapSize = new Vector2(16, 16);
        tileMap = new GameObject[(int)MapSize.x * 4, (int)MapSize.y * 4];
        newCanvas = Instantiate(canvas) as GameObject;
        SetTiles();
    }

    void SetTiles()
    {
        horizExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                GameObject newButton = Instantiate(button) as GameObject;
                var btn = newButton.GetComponent<Button>();
                newButton.transform.SetParent(newCanvas.transform, false);
                btn.image.rectTransform.sizeDelta = new Vector2(tileSize, tileSize);
                newButton.transform.position = new Vector3(x - horizExtent + .5f, -y +
                    (Camera.main.orthographicSize * 2 - 1) / 2f, 0);
                tileMap[x, y] = newButton;
                //tileMap[x,y] = GUI.Button(new Rect(x*tileSize, y*tileSize, tileSize, tileSize), "");
            }
        }
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        //Debug.Log(horizExtent);
        //Debug.Log(Camera.main.WorldToScreenPoint.)
    }

    public void setMapSize()
    {
        fullSize = !fullSize;
        Destroy(newCanvas);

        /*//clear tiles
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                Debug.Log(tileMap[x, y].GetComponent<Button>());
                Destroy(tileMap[x, y].GetComponent<Button>());
            }
        }*/
        if (fullSize)
        {
            Camera.main.orthographicSize = 16;
            tileSize = 18.5f;
            MapSize = new Vector2(32, 32);
            newCanvas = Instantiate(canvas) as GameObject;
            SetTiles();
        }
        else if (!fullSize)
        {
            Camera.main.orthographicSize = 8;
            tileSize = 37;
            MapSize = new Vector2(16, 16);
            newCanvas = Instantiate(canvas) as GameObject;
            SetTiles();
        }
    }

    public void SaveMyMap()
    {
        DeleteSavedMap();
        fileName = inputField.text;
        WriteTiles();
        cancelSave();
    }

    public void ShowSaveMenu()
    {
        saveMenu.SetActive(true);
    }


    public void cancelSave()
    {
        saveMenu.SetActive(false);
    }

    public void ShowDeleteMenu()
    {
        deleteMenu.SetActive(true);
    }
    
    public void cancelDelete()
    {
        deleteMenu.SetActive(false);
    }

    public void WriteTiles()
    {
        text = "";
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                string str = tileMap[x, y].GetComponent<Image>().sprite.name;
                text = text + str[0];
            }
            text = text + System.Environment.NewLine;
        }
        if (fileName == "") { fileName = "no name"; }
        PlayerPrefs.SetString("customMapName", fileName);

        if (fullSize)
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".txt", text);
        }
        else if (!fullSize)
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/Quarter" + fileName + ".txt", text);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".txt", ConvertMapToFullSize());
        }
    }

    public void ReadTiles()
    {
        int index = 0;
        int num = 0;
        //text = "";

        if (fullSize)
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/" + fileName + ".txt"))
            {
                text = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".txt");
            }
            else
            {
                Debug.Log("File does not exist!");
                return;
            }
        }
        else if (!fullSize)
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/Quarter" + fileName + ".txt"))
            {
                text = System.IO.File.ReadAllText(Application.persistentDataPath + "/Quarter" + fileName + ".txt");
            }
            else
            {
                Debug.Log("File does not exist!");
                return;
            }
        }
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                num = text[index] - '0';
                //ignore the new line character
                while (num < 0)
                {
                    index = index + 1;
                    num = text[index] - '0';
                }

                //set the index corresponding to sprite image as the tile image...
                tileMap[x, y].GetComponent<Image>().sprite = FindTiles(num);

                ++index;
            }
        }
    }

    Sprite FindTiles(int num)
    {
        foreach (var item in tileList)
        {
            string itemNum = item.name;
            itemNum = "" + itemNum[0];
            if (int.Parse(itemNum) == num)
            {
                return item;
            }
        }
        return null;
    }

    public void ClearTiles()
    {

        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                tileMap[x, y].GetComponent<Image>().sprite = tileList[0];
            }
        }
    }

    string ConvertMapToFullSize()
    {
        int index = 0;
        string line = "";
        string fullMap = "";
        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                int num = text[index] - '0';
                while (num < 0)
                {
                    ++index;
                    num = text[index] - '0';
                }
                line = line + text[index];
                ++index;
            }
            fullMap = fullMap + line;
            line = Reverse(line);
            fullMap = fullMap + line;
            line = "";
        }
        fullMap = fullMap + Reverse(fullMap);
        return fullMap;
    }

    public string Reverse(string s)
    {
        int num = 0;
        char[] charArray = s.ToCharArray();
        string reverse = string.Empty;
        for (int i = charArray.Length - 1; i > -1; i--)
        {
            /*if (num == 16 && s.Length < 17)
            {
                num = 0;
                reverse = reverse + System.Environment.NewLine;
            }*/
            reverse += charArray[i];
            ++num;
        }

        return reverse;
    }

    public void RandomMap()
    {
        int index = 0;
        int num = 0;
        TextAsset textFile = (TextAsset)Resources.Load("randomSeed", typeof(TextAsset));
        text = textFile.text;
        //text = System.IO.File.ReadAllText(Application.persistentDataPath + "/Maps/randomSeed.txt");
        char[] charArray = text.ToCharArray();
        for (var j = charArray.Length - 1; j > 0; j--)
        {
            var rand = UnityEngine.Random.Range(0, j);
            var tmp = charArray[j];
            charArray[j] = charArray[rand];
            charArray[rand] = tmp;
        }
        text = new string(charArray);

        for (var y = 0; y < MapSize.y; y++)
        {
            for (var x = 0; x < MapSize.x; x++)
            {
                num = text[index] - '0';

                //set the index corresponding to sprite image as the tile image...
                tileMap[x, y].GetComponent<Image>().sprite = FindTiles(num);

                ++index;
            }
        }
    }

    public void DeleteSavedMap()
    {
        if (PlayerPrefs.HasKey("customMapName"))
        {
            string str = PlayerPrefs.GetString("customMapName");

            if (System.IO.File.Exists(Application.persistentDataPath + "/" + str + ".txt"))
            {

                System.IO.File.Delete(Application.persistentDataPath + "/" + str + ".txt");

            }
            if (System.IO.File.Exists(Application.persistentDataPath + "/Quarter" + str + ".txt"))
            {
                System.IO.File.Delete(Application.persistentDataPath + "/Quarter" + str + ".txt");
            }
            PlayerPrefs.DeleteKey("customMapName");
        }
    }


}
