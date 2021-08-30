using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGeneration
{
    public class BoardGenerator : MonoBehaviour
    {
        public static BoardGenerator instance;

        public GameObject cell;

        public int baseSize = 5;

        public GameObject[,] cells;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        void Start()
        {
            cells = new GameObject[baseSize, baseSize];

            float vertDist = Mathf.Sqrt(0.75f);

            for (int i = 0; i < baseSize; i++)
            {
                for (int j = 0; j < baseSize - i; j++)
                {
                    cells[i, j] = Instantiate(cell, new Vector3(i + 0.5f * j, 0, -j * vertDist), Quaternion.identity);
                    cells[i, j].GetComponent<Cell>().index = new Vector2Int(i, j);
                }
            }
        }
    }
}