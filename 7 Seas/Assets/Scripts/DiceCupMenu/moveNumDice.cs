using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveNumDice : MonoBehaviour
{

    public Text numText;

    public void greenArrowPress()
    {
        int num = int.Parse(numText.text);
        ++num;
        numText.text = "" + num;

    }

    public void redArrowPress()
    {
        int num = int.Parse(numText.text);
        if (num != 0)
        {
            --num;
        }
        numText.text = "" + num;

    }


}
