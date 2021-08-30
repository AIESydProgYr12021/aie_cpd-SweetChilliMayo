using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject cell;

    public int baseSize = 5;

    public List<GameObject> cells = new List<GameObject>();

    void Start()
    {
        float vertDist = Mathf.Sqrt(0.75f);

        for (int i = 0; i < baseSize; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                cells.Add(Instantiate(cell, new Vector3(i - j * 0.5f - baseSize / 2 + 0.5f, 0, -j * vertDist), Quaternion.identity, transform));
            }
        }

        foreach (GameObject cell in cells)
        {
            foreach (GameObject other in cells)
            {
                if (cell == other) continue;

                if (Vector3.Distance(cell.transform.position, other.transform.position) < 1.25f)
                    cell.GetComponent<Cell>().neighbours.Add(other.GetComponent<Cell>());
            }

            BoardManager.instance.availableCells.Add(cell.GetComponent<Cell>());
        }
    }
}
