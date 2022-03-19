using UnityEngine;
using UnityEngine.UI;

public class AdvancedSetup : MonoBehaviour
{
    public Text treasureAmount;
    public Slider treasureSlider;

    float currAmount = 0;
    const string AMOUNT_TEXT = "End Game Treasure Amount: ";

    // Start is called before the first frame update
    void Start()
    {
        float amount = PlayerPrefs.GetFloat("End");

        if (amount != 0f)
        {
            treasureSlider.value = amount;
            currAmount = amount;
            treasureAmount.text = AMOUNT_TEXT + amount.ToString();
        }
    }

    public void SetTreasure()
    {
        if (treasureSlider.value != currAmount)
        {
            currAmount = treasureSlider.value;

            PlayerPrefs.SetFloat("End", currAmount);

            treasureAmount.text = AMOUNT_TEXT + currAmount.ToString();
        }
    }
}
