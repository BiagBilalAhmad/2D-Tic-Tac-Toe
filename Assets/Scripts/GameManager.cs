using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private RectTransform currentPlayerHighLight;
    [SerializeField] private bool testingIDs;

    [System.Serializable]
    public class Player_Testing
    {
        public string name;
        public string motto;
        public int point;
        public int medium;
        public int large;
        public int remover;
    }

    [SerializeField] private Player_Testing[] playerTesting = new Player_Testing[2];


    [System.Serializable]
    public class Player_
    {
        public string name;
        public string motto;
        public int point;
        public int medium;
        public int large;
        public int remover;
    }

    [Space(15)]
    [SerializeField] private Player_[] player = new Player_[2];
    [SerializeField] private GamePlayUI gamePlayUI;

    public enum Player { None, Circle, Cross }

    public TextMeshProUGUI winText;
    public GameObject restartButton;
    public Action GameplayRestarted;

    private Player[,] grid = new Player[3, 3];
    private Player currentPlayer = Player.Circle;

    public bool GetCurrentPlayer() { if (currentPlayer == Player.Circle) { return true; } else { return false; } }

    public void UpdatePlayer(bool player1, string name, string motto, int point, int medium, int large, int remover)
    {
        if (player1)
        {
            player[0].name = name;
            player[0].motto = motto;
            player[0].point = point;
            player[0].medium = medium;
            player[0].large = large;
            player[0].remover = remover;
            gamePlayUI.UpdatePoints1(name, motto, point.ToString());
        }
        else
        {
            player[1].name = name;
            player[1].motto = motto;
            player[1].point = point;
            player[1].medium = medium;
            player[1].large = large;
            player[1].remover = remover;
            gamePlayUI.UpdatePoints2(name, motto, point.ToString());
        }


        UpdateGameUI(player1);
    }

    public bool CheckeMove(int size)
    {
        int i = 0;

        if (!GetCurrentPlayer())
        {
            i = 1;
        }

        switch (size)
        {
            case 1:
                if(player[i].medium > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (player[i].large > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                if (player[i].remover > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default: return true;
        }
    }
    public void ReduceMove(int size)
    {
        int i = 0;

        if (!GetCurrentPlayer())
        {
            i = 1;
        }

        switch (size)
        {
            case 1:
                if (player[i].medium > 0)
                {
                    player[i].medium--;
                }
                break;
            case 2:
                if (player[i].large > 0)
                {
                    player[i].large--;
                }
                break;
            case 3:
                if (player[i].remover > 0)
                {
                    player[i].remover--;
                }
                break;
        }
    }

    private void UpdateGameUI(bool player1)
    {
        if(player1)
        {
            gamePlayUI.UpdateGamePlayUI1(player[0].medium.ToString(), player[0].large.ToString(), player[0].remover.ToString());
        }
        else
        {
            gamePlayUI.UpdateGamePlayUI2( player[1].medium.ToString(), player[1].large.ToString(), player[1].remover.ToString());
        }
    }

    private void OnEnable()
    {
        Instance = this;
        StartNewGame();
    }

    private void OnDisable()
    {
        RestartGame();
    }

    public void StartNewGame()
    {
        if(GameplayRestarted != null)
        {
            GameplayRestarted();
        }

        currentPlayer = Player.Circle; // Circle starts first

        UpdateCurrentPlayerText();
        winText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        if (testingIDs)
        {
            UpdatePlayer(true, playerTesting[0].name, playerTesting[0].motto, playerTesting[0].point, playerTesting[0].medium, playerTesting[0].large, playerTesting[0].remover);
            UpdatePlayer(false, playerTesting[1].name, playerTesting[1].motto, playerTesting[1].point, playerTesting[1].medium, playerTesting[1].large, playerTesting[1].remover);
        }
        else
        {
            // Retrieve Player IDs
        }

        ClearGrid();
    }

    public void PlaceMark(int row, int col)
    {
        grid[row, col] = currentPlayer;
        if (CheckWinCondition() || CheckDrawCondition())
        {
            return;
        }
        SwitchPlayer();
    }
    public void RemoveMark(int row, int col)
    {
        grid[row, col] = Player.None;

        SwitchPlayer();
    }

    private bool CheckWinCondition()
    {
        // Check rows, columns, and diagonals for a win
        for (int i = 0; i < 3; i++)
        {
            if (grid[i, 0] != Player.None && grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2])
            {
                EndGame(grid[i, 0]);
                return true;
            }
            if (grid[0, i] != Player.None && grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
            {
                EndGame(grid[0, i]);
                return true;
            }
        }

        if (grid[0, 0] != Player.None && grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
        {
            EndGame(grid[0, 0]);
            return true;
        }
        if (grid[0, 2] != Player.None && grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
        {
            EndGame(grid[0, 2]);
            return true;
        }

        return false;
    }

    private bool CheckDrawCondition()
    {
        // Check if the grid is full and there is no winner
        foreach (Player player in grid)
        {
            if (player == Player.None)
            {
                return false; // Grid is not full
            }
        }
        EndGame(Player.None); // Draw
        return true;
    }

    private void SwitchPlayer()
    {
        UpdateGameUI(GetCurrentPlayer());
        currentPlayer = (currentPlayer == Player.Circle) ? Player.Cross : Player.Circle;
        UpdateCurrentPlayerText();
    }

    private void UpdateCurrentPlayerText()
    {
        if (currentPlayer == Player.Circle)
        {
            currentPlayerHighLight.DOLocalMoveX(-269.5f, 0.2f).SetEase(Ease.Linear);
        }
        else
        {
            currentPlayerHighLight.DOLocalMoveX(269.5f, 0.2f).SetEase(Ease.Linear);
        }
    }

    private void EndGame(Player winner)
    {
        winText.gameObject.SetActive(true);
        restartButton.SetActive(true);

        if (winner == Player.None)
        {
            winText.text = "Draw!";
        }
        else
        {
            winText.text = "Player " + winner.ToString() + " Wins!";
        }
    }

    public void RestartGame()
    {
        StartNewGame();
    }

    private void ClearGrid()
    {
        // Reset the grid
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grid[i, j] = Player.None;
            }
        }
    }
}
