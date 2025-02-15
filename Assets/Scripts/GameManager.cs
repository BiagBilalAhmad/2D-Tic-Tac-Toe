using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private RectTransform currentPlayerHighLight;
    [SerializeField] private bool testingIDs;

    public int coins;

    [Header("Powerups")]
    public bool hasPowerup;

    public int large, medium, remover;

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
    public GameObject resumeButton;
    public event Action GameplayRestarted;

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

    public List<int> GetMoves()
    {
        int i = 0;

        if (!GetCurrentPlayer())
        {
            i = 1;
        }

        List<int> playerMoves = new List<int>();
        playerMoves.Clear();
        playerMoves.Add(player[i].medium);
        playerMoves.Add(player[i].large);
        playerMoves.Add(player[i].remover);

        return playerMoves;
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
            Debug.Log("Game Restarted!");
            GameplayRestarted.Invoke();
        }

        coins = PlayerPrefs.GetInt("Coins", 0);

        //int power = PlayerPrefs.GetInt("Powerup", 0);
        //hasPowerup = power == 1 ? true : false;

        //large = PlayerPrefs.GetInt("LargePowerup", 0);
        //medium = PlayerPrefs.GetInt("MediumPowerup", 0);
        //remover = PlayerPrefs.GetInt("RemoverPowerup", 0);

        //if (!hasPowerup)
        //{
        //    player[0].medium = 0;
        //    player[0].large = 0;
        //    player[0].remover = 0;

        //    player[1].medium = 0;
        //    player[1].large = 0;
        //    player[1].remover = 0;
        //}

        //if (hasPowerup)
        //{
        //    player[0].medium = medium;
        //    player[0].large = large;
        //    player[0].remover = remover;

        //    player[1].medium = medium;
        //    player[1].large = large;
        //    player[1].remover = remover;
        //}

        currentPlayer = Player.Circle; // Circle starts first

        UpdateCurrentPlayerText();
        winText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        resumeButton.SetActive(false);

        if (testingIDs)
        {
            UpdatePlayer(true, player[0].name, player[0].motto, player[0].point, player[0].medium, player[0].large, player[0].remover);
            UpdatePlayer(false, player[1].name, player[1].motto, player[1].point, player[1].medium, player[1].large, player[1].remover);
        }
        else
        {
            // Retrieve Player IDs
        }

        ClearGrid();
    }

    public void CallGameRestarted()
    {
        Debug.Log("Game Restarted!");
        GameplayRestarted.Invoke();
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
            coins += UnityEngine.Random.Range(100, 150);
            PlayerPrefs.SetInt("Coins", coins);
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
