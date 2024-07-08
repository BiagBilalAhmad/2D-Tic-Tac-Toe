using UnityEngine;

public class TTTButtonHolder : MonoBehaviour
{
    public static TTTButtonHolder Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] tSelectionPanels;
    public TTTButtonChecker[] tButtons;

    public void HideTTTSelection()
    {
        foreach (var selection in tSelectionPanels)
        {
            selection.gameObject.SetActive(false);
        }
    }

    public void UpdateTTTSelectionUI()
    {
        foreach (var tbutton in tButtons)
        {
            tbutton.UpdateSelectionUI();
        }
    }

    public void ResetTTTSelectionUI()
    {
        foreach (var tbutton in tButtons)
        {
            tbutton.ResetSelectIntracable();
        }
    }
}
