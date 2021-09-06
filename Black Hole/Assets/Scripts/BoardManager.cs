using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour
{
    static public BoardManager instance;

    public int currentPlayer = 1;
    public Material player1;
    public Material player2;

    public List<int> numberList1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public List<int> numberList2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public List<Cell> availableCells = new List<Cell>();

    public GameObject winScreen;
    public TMP_Text player1Text;
    public TMP_Text player2Text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError($"{GetType().Name} already has an instance!");
        }
    }

    public void CheckState()
    {
        if (availableCells.Count == 1)
        {
            SinkCell();
        }
    }

    int player1Score = 0;
    int player2Score = 0;

    public void SinkCell()
    {
        if (availableCells[0].neighbours.Count < 1) 
        {
            player1Text.text = player1Score.ToString();
            player2Text.text = player2Score.ToString();
            winScreen.SetActive(true);
            return; 
        }

        Cell cell = availableCells[0].neighbours[0];

        if (cell.colorMaterial == player1)
            player1Score += cell.GetNumber();
        else
            player2Score += cell.GetNumber();

        cell.StartSinking();

        availableCells[0].neighbours.Remove(cell);
    }
}
