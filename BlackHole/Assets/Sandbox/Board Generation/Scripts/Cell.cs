using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGeneration
{
    public class Cell : MonoBehaviour
    {
        public Vector2Int index;

        public Material normal;
        public Material highlighted;

        readonly Vector2Int[] offsets =
        {
            new Vector2Int(0, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 0),
        };

        private void Start()
        {
            normal = GetComponent<Renderer>().material;
        }

        private void OnMouseOver()
        {
            foreach (GameObject validCell in GetNeighbours())
                validCell.GetComponent<Renderer>().material = highlighted;
        }

        private void OnMouseExit()
        {
            foreach (GameObject validCell in GetNeighbours())
                validCell.GetComponent<Renderer>().material = normal; ;
        }

        private GameObject[] GetNeighbours()
        {
            List<GameObject> neighbours = new List<GameObject>();

            foreach(Vector2Int offset in offsets)
                if (IsValidIndex(index + offset))
                    neighbours.Add(BoardGenerator.instance.cells[index.x + offset.x, index.y + offset.y]);

            return neighbours.ToArray();
        }

        private bool IsValidIndex(Vector2Int index)
        {
            return index.x >= 0 && index.y >= 0 &&
                index.x < 6 - index.y;
        }
    }
}