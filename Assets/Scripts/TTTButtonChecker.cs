using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Start()
    {
        GameManager.Instance.GameplayRestarted += ButtonReset;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GameplayRestarted -= ButtonReset;
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

            if (move == 0 && i)
            {
                GameManager.Instance.ReduceMove(move);
                playerCheck = player1;
                CheckCrossSmall[boxCheck - 1].SetActive(true);
                UpdateButtons();
                move++;
                GameManager.Instance.PlaceMark(row, column);
            }
            else if (move == 1 && i)
            {
                if (playerCheck != player1)
                {
                    GameManager.Instance.ReduceMove(move);
                    playerCheck = player1;
                    CheckCrossMedium[boxCheck - 1].SetActive(true);
                    UpdateButtons();
                    move++;
                    GameManager.Instance.PlaceMark(row, column);
                }
            }
            else if (move == 2 && i)
            {
                if (playerCheck != player1)
                {
                    GameManager.Instance.ReduceMove(move);
                    playerCheck = player1;
                    CheckCrossLarge[boxCheck - 1].SetActive(true);
                    UpdateButtons();
                    move++;
                    GameManager.Instance.PlaceMark(row, column);
                }
            }
            else if (move == 3 && i)
            {
                if (playerCheck != player1)
                {
                    GameManager.Instance.ReduceMove(move);
                    playerCheck = player1;
                    GameManager.Instance.RemoveMark(row, column);
                    ButtonReset();
                }
            }
        }
    }
    public void ButtonReset()
    {
        move = 0;
        boxCheck = 0;
        player1 = true;

        foreach(GameObject obj in CheckCrossSmall)
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
    }

    private void UpdateButtons()
    {
        int i = 0;

        if(boxCheck == 1)
        {
            i = 1;
        }
        else
        {
            i = 0;
        }

        if (move == 1)
        {
            CheckCrossSmall[i].SetActive(false);
        }
        else if (move == 2)
        {
            CheckCrossMedium[i].SetActive(false);
        }
    }
}
