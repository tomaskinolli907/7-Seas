using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class LoadMap : MonoBehaviour {

    public TextAsset textFile;
    public byte[] byteText;
    public string text;
    //public string line;

    void Awake()
    {
        //text = System.IO.File.ReadAllText(Application.persistentDataPath + "/map2.txt");
        //textFile = (TextAsset)Resources.Load("PiratesCove", typeof(TextAsset));
        //text = textFile.text;
        text = PlayerPrefs.GetString("mapText");
    }

    // Use this for initialization
    void Start () {

        //text = textFile.text;

        //byteText = textFile.bytes;

    }
	
}
