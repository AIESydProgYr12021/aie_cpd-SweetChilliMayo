using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neighbours = new List<Cell>();

    public TextMesh text;

    public Material defaultMaterial;
    public Material colorMaterial;

    public Renderer ringRenderer;

    int number = 0;

    bool isSinking = false;

    void SetNumber(int number, Material material)
    {
        if (number != 0)
        {
            this.number = number;
            text.text = number.ToString();

            colorMaterial = material;
            ringRenderer.material = material;
        }
        else
        {
            this.number = 0;
            text.text = "";

            colorMaterial = defaultMaterial;
            ringRenderer.material = defaultMaterial;
        }
    }

    public int GetNumber()
    {
        return number;
    }

    private void OnMouseDown()
    {
        if (number != 0) return;

        BoardManager manager = BoardManager.instance;

        if (manager.currentPlayer == 1)
        {
            SetNumber(manager.numberList1[0], manager.player1);
            manager.RemoveBlue(0);
            manager.numberList1.RemoveAt(0);
            manager.currentPlayer = 2;
        }
        else
        {
            SetNumber(manager.numberList2[0], manager.player2);
            manager.RemoveRed(0);
            manager.numberList2.RemoveAt(0);
            manager.currentPlayer = 1;
        }

        manager.availableCells.Remove(this);

        manager.CheckState();
    }

    float lerpTime = 0;

    private void Update()
    {
        if (isSinking)
        {
            lerpTime += Time.deltaTime;

            if (lerpTime > 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, BoardManager.instance.availableCells[0].transform.position, 0.05f);
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.05f);

                if (Vector3.Distance(transform.position, BoardManager.instance.availableCells[0].transform.position) < 0.5f)
                {
                    BoardManager.instance.SinkCell();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void StartSinking()
    {
        isSinking = true;
    }
}
