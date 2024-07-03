using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI name1;
    [SerializeField] private TextMeshProUGUI motto1;

    [SerializeField] private TextMeshProUGUI mediumAvailable;
    [SerializeField] private TextMeshProUGUI largeAvailable;
    [SerializeField] private TextMeshProUGUI removerAvailable;

    [Space(15)]

    [SerializeField] private TextMeshProUGUI points2;
    [SerializeField] private TextMeshProUGUI name2;
    [SerializeField] private TextMeshProUGUI motto2;

    [SerializeField] private TextMeshProUGUI mediumAvailable2;
    [SerializeField] private TextMeshProUGUI largeAvailable2;
    [SerializeField] private TextMeshProUGUI removerAvailable2;

    public void UpdatePoints1(string name, string motto, string point)
    {
        points.text = point;
        name1.text = name;
        motto1.text = motto;
    }
    public void UpdatePoints2(string name, string motto, string point)
    {
        points2.text = point;
        name2.text = name;
        motto2.text = motto;
    }
    public void UpdateGamePlayUI1(string mediumA, string largeA, string remover)
    {
        mediumAvailable.text = mediumA;
        largeAvailable.text = largeA;
        removerAvailable.text = remover;
    }
    public void UpdateGamePlayUI2(string mediumA, string largeA, string remover)
    {
        mediumAvailable2.text = mediumA;
        largeAvailable2.text = largeA;
        removerAvailable2.text = remover;
    }
}
