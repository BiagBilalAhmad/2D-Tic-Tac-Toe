using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTTButtonChecker : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int column;

    private int boxCheck = 0;
    private bool player1 = true;
    private bool playerCheck = false;

    [SerializeField] private GameObject[] CheckCrossSmall = new GameObject[2];
    [SerializeField] private GameObject[] CheckCrossMedium = new GameObject[2];
    [SerializeField] private GameObject[] CheckCrossLarge = new GameObject[2];
    private int move = 0;

    [Header("Selection Panel")]
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private GameObject[] SelectionCheckCrossMedium = new GameObject[2];
    [SerializeField] private GameObject[] SelectionCheckCrossLarge = new GameObject[2];
    [SerializeField] private TMP_Text selectionMediumTMP;
    [SerializeField] private TMP_Text selectionLargeTMP;
    [SerializeField] private TMP_Text selectionRemoverTMP;
    [SerializeField] private Button selectionMediumButton;
    [SerializeField] private Button selectionLargeButton;
    [SerializeField] private Button selectionRemoverButton;

    private int large, medium, remover;
    public List<int> playerMoves;

    private void Start()
    {
        
        selectionPanel.SetActive(false);
        //GameManager.Instance.GameplayRestarted += ButtonReset;
    }
    private void OnDisable()
    {
        //GameManager.Instance.GameplayRestarted -= ButtonReset;
    }

    public void ButtonCheckCross()
    {
        if (move <= 3)
        {
            // checks whose player turn it is

            player1 = GameManager.Instance.GetCurrentPlayer();


            if (player1)
            {
                boxCheck = 1;
            }
            else
            {
                boxCheck = 2;
            }

            bool i = GameManager.Instance.CheckeMove(move);
            TTTButtonHolder.Instance.HideTTTSelection();
            TTTButtonHolder.Instance.UpdateTTTSelectionUI();

            if (move == 0 && i)
            {
                playerCheck = player1;
                UpdateButtons();
                CheckCrossSmall[boxCheck - 1].SetActive(true);
                move++;
                GameManager.Instance.PlaceMark(row, column);
            }
            else
            {
                //for (int j = 0; j < selectionPanel.transform.childCount; j++)
                //{
                UpdateSelectionMarks();
                //UpdateSelectionUI();
                selectionPanel.SetActive(true);
                SelectionCheckCrossMedium[boxCheck - 1].SetActive(true);
                SelectionCheckCrossLarge[boxCheck - 1].SetActive(true);
                //}
            }
            //else if (move == 1 && i)
            //{
            //PutMediumMark();
            //}
            //else if (move == 2 && i)
            //{
            //PutLargeMark();
            //}
            //else if (move == 3 && i)
            //{
            //    if (playerCheck != player1)
            //    {
            //RemoveMark();
            //    }
            //}
        }
    }

    public void PutMediumMark()
    {
        if (playerCheck != player1)
        {
            TTTButtonHolder.Instance.HideTTTSelection();
            GameManager.Instance.ReduceMove(1);
            playerCheck = player1;
            UpdateButtons();
            CheckCrossMedium[boxCheck - 1].SetActive(true);
            move++;
            GameManager.Instance.PlaceMark(row, column);
        }
    }

    public void PutLargeMark()
    {
        if (playerCheck != player1)
        {
            TTTButtonHolder.Instance.HideTTTSelection();
            GameManager.Instance.ReduceMove(2);
            playerCheck = player1;
            UpdateButtons();
            CheckCrossLarge[boxCheck - 1].SetActive(true);
            move++;
            GameManager.Instance.PlaceMark(row, column);
        }
    }

    public void RemoveMark()
    {
        TTTButtonHolder.Instance.HideTTTSelection();
        GameManager.Instance.ReduceMove(3);
        playerCheck = player1;
        GameManager.Instance.RemoveMark(row, column);
        ButtonReset();
    }

    public void ButtonReset()
    {
        move = 0;
        boxCheck = 0;
        player1 = true;
        TTTButtonHolder.Instance.ResetTTTSelectionUI();
        UpdateButtons();
    }

    public void ResetSelectIntracable()
    {
        selectionMediumButton.interactable = true;
        selectionLargeButton.interactable = true;
        selectionRemoverButton.interactable = true;
    }

    private void UpdateButtons()
    {
        //int i = 0;

        //if(boxCheck == 1)
        //{
        //    i = 1;
        //}
        //else
        //{
        //    i = 0;
        //}

        foreach (GameObject obj in CheckCrossSmall)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in CheckCrossMedium)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in CheckCrossLarge)
        {
            obj.SetActive(false);
        }


        //CheckCrossSmall[i].SetActive(false);
        //CheckCrossMedium[i].SetActive(false);
    }

    private void UpdateSelectionMarks()
    {
        //int i = 0;

        //if (boxCheck == 1)
        //{
        //    i = 1;
        //}
        //else
        //{
        //    i = 0;
        //}

        foreach (GameObject obj in SelectionCheckCrossMedium)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in SelectionCheckCrossLarge)
        {
            obj.SetActive(false);
        }
        //SelectionCheckCrossMedium[i].SetActive(false);
        //SelectionCheckCrossLarge[i].SetActive(false);
    }

    public void UpdateSelectionUI()
    {
        playerMoves.Clear();
        playerMoves = GameManager.Instance.GetMoves();
        medium = playerMoves[0];
        large = playerMoves[1];
        remover = playerMoves[2];

        Debug.Log("Selection UI Upadted!");
        selectionMediumTMP.text = medium.ToString();
        selectionLargeTMP.text = large.ToString();
        selectionRemoverTMP.text = remover.ToString();

        if (medium <= 0)
        {
            selectionMediumButton.interactable = false;
        }

        if (large <= 0)
        {
            selectionLargeButton.interactable = false;
        }

        if (remover <= 0)
        {
            selectionRemoverButton.interactable = false;
        }
    }
}
