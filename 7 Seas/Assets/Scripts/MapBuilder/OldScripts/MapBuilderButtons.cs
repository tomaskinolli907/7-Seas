using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBuilderButtons : MonoBehaviour
{
    static Sprite sprite;
    List<Image> highlightArray;

	// Use this for initialization
	void Start ()
    {
        highlightArray = new List<Image>();
        var highlights = GameObject.FindGameObjectsWithTag("Highlight");
        foreach (var item in highlights)
        {
            highlightArray.Add(item.GetComponent<Image>());
        }
	}


    public void GetImage()
    {
        foreach(var item in highlightArray)
        {
            item.enabled = false;
        }
        var highlight = this.GetComponentsInChildren<Image>();
        highlight[1].enabled = true;
        sprite = this.GetComponent<Image>().sprite;
    }

    public void SetImage()
    {

        if (sprite != null)
        {
            this.GetComponent<Image>().sprite = sprite;
        }
    }

    public void SetImageMouseDown()
    {
        if(Input.GetMouseButton(0))
        {
            if (sprite != null && this.GetComponent<Image>().sprite != sprite)
            {
                this.GetComponent<Image>().sprite = sprite;
            }
        }
    }



}
