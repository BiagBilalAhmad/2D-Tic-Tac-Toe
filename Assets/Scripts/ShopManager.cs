using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TMP_Text coinTxt;

    public Button checkPowerup;
    public Button crossPowerup;

    public int coins;
    public bool canBuyMore;

    void Start()
    {
        coins = 2000; // PlayerPrefs.GetInt("Coins", 0);
        coinTxt.text = coins.ToString();

        int power = PlayerPrefs.GetInt("Powerup", 0);
        canBuyMore = power == 0 ? true : false;

        if (coins < 300 || !canBuyMore)
        {
            checkPowerup.interactable = false;
            crossPowerup.interactable = false;
        }
    }

    public void BuyCheckPowerup()
    {
        if (coins >= 300 && canBuyMore)
        {
            coins -= 300;
            coinTxt.text = coins.ToString();

            PlayerPrefs.SetInt("Coins", coins);

            PlayerPrefs.SetInt("Powerup", 1);

            canBuyMore = false;
        }

        if (coins < 300 || !canBuyMore)
        {
            checkPowerup.interactable = false;
            crossPowerup.interactable = false;
        }
    }
}
