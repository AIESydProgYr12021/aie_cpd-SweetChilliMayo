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

    public GameObject placeEffectBlue;
    public GameObject placeEffectRed;

    public GameObject blueRing;
    public GameObject redRing;

    public AnimationCurve curve;
    public float height = 1f;

    public bool canPlace = true;

    int player1Score = 0;
    int player2Score = 0;

    float lerpTime = 0.0f;

    Cell target;
    int num;
    Material mat;
    GameObject currentMovingCell;
    Vector3 movingCellStart;
    Vector3 movingCellEnd;
    Quaternion movingCellStartRot;
    Quaternion movingCellEndRot = Quaternion.Euler(0, 0, 0);
    bool effectPlaced = false;

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
        blueRing.SetActive(currentPlayer == 1 && canPlace);
        redRing.SetActive(currentPlayer == 2 && canPlace);

        if (currentMovingCell == null)
        {
            lerpTime = 0.0f;
            return;
        }

        lerpTime += Time.deltaTime;

        if (lerpTime < 1.0f)
        {
            currentMovingCell.transform.position = Vector3.Lerp(movingCellStart, movingCellEnd, lerpTime) + new Vector3(0, curve.Evaluate(lerpTime) * height, 0);
            currentMovingCell.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.1f, 0.5f), new Vector3(1f, 0.12f, 1f), lerpTime);
        }
        else if (lerpTime < 2f)
        {
            if (!effectPlaced)
            {
                if (currentPlayer == 1)
                    Instantiate(placeEffectRed, target.transform.position, Quaternion.identity);
                else
                    Instantiate(placeEffectBlue, target.transform.position, Quaternion.identity);

                effectPlaced = true;
            }

            currentMovingCell.transform.rotation = Quaternion.Lerp(movingCellStartRot, movingCellEndRot, lerpTime - 1f);
            currentMovingCell.transform.localScale = Vector3.Lerp(new Vector3(1f, 0.12f, 1f), new Vector3(1f, 0.1f, 1f), lerpTime - 1f);
        }
        else
        {
            target.SetNumber(num, mat);
            Destroy(currentMovingCell);
            movingCellStart = Vector3.zero;
            movingCellEnd = Vector3.zero;
            lerpTime = 0.0f;
            effectPlaced = false;
            canPlace = true;
            CheckState();
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
            if (player1Score == player2Score)
                winText.text = $"TIE!";
            else
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

    public void PlaceRed(int index, Cell target, int number, Material material)
    {
        this.target = target;
        num = number;
        mat = material;
        movingCellEnd = target.transform.position;
        currentMovingCell = redNumbers[index];
        currentMovingCell.transform.SetParent(null);
        movingCellStart = currentMovingCell.transform.position;
        movingCellStartRot = currentMovingCell.transform.rotation;
        redNumbers.RemoveAt(index);
    }

    public void PlaceBlue(int index, Cell target, int number, Material material)
    {
        this.target = target;
        num = number;
        mat = material;
        movingCellEnd = target.transform.position;
        currentMovingCell = blueNumbers[index];
        currentMovingCell.transform.SetParent(null);
        movingCellStart = currentMovingCell.transform.position;
        movingCellStartRot = currentMovingCell.transform.rotation;
        blueNumbers.RemoveAt(index);
    }
}
