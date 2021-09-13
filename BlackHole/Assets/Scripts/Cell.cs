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

    public AudioClip clickSound;
    public AudioClip placeSound;

    int number = 0;

    bool isSinking = false;

    public void SetNumber(int number, Material material)
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
        BoardManager manager = BoardManager.instance;

        if (number != 0 || !manager.canPlace) return;

        AudioManager.Instance.PlaySound(clickSound);

        manager.canPlace = false;

        if (manager.currentPlayer == 1)
        {
            manager.PlaceBlue(0, this, manager.numberList1[0], manager.player1);
            manager.numberList1.RemoveAt(0);
            manager.currentPlayer = 2;
        }
        else
        {
            manager.PlaceRed(0, this, manager.numberList2[0], manager.player2);
            manager.numberList2.RemoveAt(0);
            manager.currentPlayer = 1;
        }

        manager.availableCells.Remove(this);
    }

    private void OnMouseEnter()
    {
        BoardManager manager = BoardManager.instance;

        if (colorMaterial != defaultMaterial)
            ringRenderer.material = colorMaterial;
        else if (!manager.canPlace)
            ringRenderer.material = defaultMaterial;
        else
            ringRenderer.material = manager.currentPlayer == 1 ? manager.player1 : manager.player2;
    }

    private void OnMouseExit()
    {
        ringRenderer.material = colorMaterial;
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
