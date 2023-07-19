using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private GameObject straightGridCellPrefab;
    [SerializeField] private GameObject angleGridCellPrefab;
    [SerializeField] private GameObject threeSidesGridCellPrefab;
    [SerializeField] private GameObject pointGridCellPrefab;

    private int height = 4;
    private int width = 10;
    private float offsetX = 6.125f;
    private float offsetY = 2.25f;
    private float gridSpaceSize = 1.25f;

    public static GameObject[,] gameGrid;

    System.Random rnd = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        GameObject[] cellPrefabs = { straightGridCellPrefab, angleGridCellPrefab, threeSidesGridCellPrefab };

        gameGrid = new GameObject[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int randomIndex = rnd.Next(cellPrefabs.Length);
                if (cellPrefabs[randomIndex] != null)
                {
                    if (x == 0 && y == 0)
                    {
                        gameGrid[x, y] = Instantiate(pointGridCellPrefab, new Vector3(x * gridSpaceSize - offsetX, y * gridSpaceSize - offsetY, -5), Quaternion.identity);
                        gameGrid[x, y].transform.parent = transform;
                    }

                    else if (x == width-1 && y == height-1)
                    {
                        gameGrid[x, y] = Instantiate(pointGridCellPrefab, new Vector3(x * gridSpaceSize - offsetX, y * gridSpaceSize - offsetY, -5), Quaternion.identity);
                        gameGrid[x, y].transform.parent = transform;
                    }

                    else
                    {
                        gameGrid[x, y] = Instantiate(cellPrefabs[randomIndex], new Vector3(x * gridSpaceSize - offsetX, y * gridSpaceSize - offsetY, -5), Quaternion.identity);
                        gameGrid[x, y].transform.parent = transform;
                    }
                }
                else
                    Debug.Log("Maaaan, Grid Cell Prefab is empty :)");
            }
        }
    }
}
