using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TMP_Text coinTxt;

    public Button largePowerup;
    public Button mediumPowerup;
    public Button removerPowerup;

    public int coins;

    public int large, medium, remover;

    void Start()
    {
        coins = 2000; // PlayerPrefs.GetInt("Coins", 0);
        coinTxt.text = coins.ToString();

        large = PlayerPrefs.GetInt("LargePowerup", 0);
        medium = PlayerPrefs.GetInt("MediumPowerup", 0);
        remover = PlayerPrefs.GetInt("RemoverPowerup", 0);
        ToggleButtonsIntractable();
    }

    private void ToggleButtonsIntractable()
    {
        if (coins < 300)
        {
            largePowerup.interactable = false;
        }

        if (coins < 200)
        {
            mediumPowerup.interactable = false;
        }

        if (coins < 300)
        {
            removerPowerup.interactable = false;
        }
    }

    public void BuyLargePowerup()
    {
        if (coins >= 300)
        {
            coins -= 300;
            coinTxt.text = coins.ToString();

            PlayerPrefs.SetInt("Coins", coins);

            large++;
            PlayerPrefs.SetInt("LargePowerup", large);

            PlayerPrefs.SetInt("Powerup", 1);
        }

        ToggleButtonsIntractable();
    }

    public void BuyMediumPowerup()
    {
        if (coins >= 200)
        {
            coins -= 200;
            coinTxt.text = coins.ToString();

            PlayerPrefs.SetInt("Coins", coins);

            medium++;
            PlayerPrefs.SetInt("MediumPowerup", large);

            PlayerPrefs.SetInt("Powerup", 1);
        }

        ToggleButtonsIntractable();
    }

    public void BuyRemoverPowerup()
    {
        if (coins >= 300)
        {
            coins -= 300;
            coinTxt.text = coins.ToString();

            PlayerPrefs.SetInt("Coins", coins);

            remover++;
            PlayerPrefs.SetInt("RemoverPowerup", large);

            PlayerPrefs.SetInt("Powerup", 1);
        }

        ToggleButtonsIntractable();
    }
}
