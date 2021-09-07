using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardManager : MonoBehaviour
{
    static public BoardManager instance;

    public int currentPlayer = 1;
    public Material player1;
    public Material player2;

    public List<int> numberList1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public List<int> numberList2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public List<GameObject> blueNumbers = new List<GameObject>();
    public List<GameObject> redNumbers = new List<GameObject>();

    public List<Cell> availableCells = new List<Cell>();

    public GameObject winScreen;
    public TMP_Text player1Text;
    public TMP_Text player2Text;
    public TMP_Text winText;

    public Sprite ring;
    public Sprite filledRing;

    int player1Score = 0;
    int player2Score = 0;

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

    private void Update()
    {
        foreach (GameObject token in blueNumbers)
        {
            token.GetComponent<Image>().sprite = currentPlayer == 1 ? filledRing : ring;
        }

        foreach (GameObject token in redNumbers)
        {
            token.GetComponent<Image>().sprite = currentPlayer == 2 ? filledRing : ring;
        }
    }

    public void CheckState()
    {
        if (availableCells.Count == 1)
        {
            SinkCell();
        }
    }

    public void SinkCell()
    {
        if (availableCells[0].neighbours.Count < 1) 
        {
            player1Text.text = player1Score.ToString();
            player2Text.text = player2Score.ToString();
            winText.text = $"{(player1Score < player2Score ? "Player 1" : "Player 2")} wins!";

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

    public void RemoveRed(int index)
    {
        Destroy(redNumbers[index]);
        redNumbers.RemoveAt(index);
    }

    public void RemoveBlue(int index)
    {
        Destroy(blueNumbers[index]);
        blueNumbers.RemoveAt(index);
    }
}
