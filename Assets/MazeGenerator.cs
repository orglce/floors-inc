using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeGenerator : MonoBehaviour
{

public class Cell
{
public int x;
public int z;

public String sideComparedToPrevious = "";

public bool visited = false;

public bool wallUp = true;
public bool wallDown = true;
public bool wallLeft = true;
public bool wallRight = true;
}

float hallwayWidth = 6f;
Stack<Cell> stack = new Stack<Cell>();

public Material fenceMaterial;
public Material linesFenceMaterial;

public GameObject buttonPrefab;

void Start()
{
        Vector3 size = GetComponent<Renderer>().bounds.size;
        float x = size.x;
        float z = size.z;

        int howManyCellsX = (int) (x/hallwayWidth);
        int howManyCellsZ = (int) (x/hallwayWidth);

        Cell[,] cells = new Cell[howManyCellsX, howManyCellsZ];

        for(int i = 0; i < howManyCellsX; i++) {
                for(int j = 0; j < howManyCellsZ; j++) {
                        cells[i, j] = new Cell();
                        cells[i, j].x = i;
                        cells[i, j].z = j;
                }
        }

        Cell startingCell = cells[0,0];
        startingCell.visited = true;
        int howMayUnvisitedLeft = howManyCellsX * howManyCellsZ - 1;
        stack.Push(startingCell);

        while (true)
        {
                Cell currentCell = stack.Peek();
                List<Cell> neighbours = new List<Cell>();

                // north
                if (currentCell.x > 0 && !cells[currentCell.x-1, currentCell.z].visited)
                {
                        neighbours.Add(cells[currentCell.x-1, currentCell.z]);
                        cells[currentCell.x-1, currentCell.z].sideComparedToPrevious = "west";

                }
                // south
                if (currentCell.x < howManyCellsX-1 && !cells[currentCell.x+1, currentCell.z].visited)
                {
                        neighbours.Add(cells[currentCell.x+1, currentCell.z]);
                        cells[currentCell.x+1, currentCell.z].sideComparedToPrevious = "east";
                }
                // west
                if (currentCell.z > 0 && !cells[currentCell.x, currentCell.z-1].visited)
                {
                        neighbours.Add(cells[currentCell.x, currentCell.z-1]);
                        cells[currentCell.x, currentCell.z-1].sideComparedToPrevious = "south";
                }
                // east
                if (currentCell.z < howManyCellsZ-1 && !cells[currentCell.x, currentCell.z+1].visited)
                {
                        neighbours.Add(cells[currentCell.x, currentCell.z+1]);
                        cells[currentCell.x, currentCell.z+1].sideComparedToPrevious = "north";
                }

                if (neighbours.Count != 0)
                {
                        System.Random rnd = new System.Random();
                        int direction  = rnd.Next(neighbours.Count);

                        Cell neighbour = neighbours[direction];

                        Debug.Log((String) neighbour.sideComparedToPrevious);
                        Debug.Log(neighbour.x + " " + neighbour.z);

                        switch (neighbour.sideComparedToPrevious)
                        {
                        case "north":
                                currentCell.wallUp = false;
                                neighbour.wallDown = false;
                                break;
                        case "south":
                                currentCell.wallDown = false;
                                neighbour.wallUp = false;
                                break;
                        case "east":
                                currentCell.wallRight = false;
                                neighbour.wallLeft = false;
                                break;
                        case "west":
                                currentCell.wallLeft = false;
                                neighbour.wallRight = false;
                                break;
                        }

                        neighbour.visited = true;
                        howMayUnvisitedLeft--;
                        stack.Push(neighbour);
                }
                else
                        stack.Pop();

                if (howMayUnvisitedLeft == 0)
                        break;
        }
        RenderMaze(cells, size);

}

void RenderMaze(Cell[,] cells, Vector3 size)
{
        GameObject grid = new GameObject("Grid");

        float wallWidth = size.x / cells.GetLength(0);
        float wallHeight = 5f;

        for(int i = 0; i < size.x/wallWidth; i++) {
                for(int j = 0; j < size.z/wallWidth; j++) {

                        Cell cell = cells[i, j];

                        if (cell.wallUp == true) {
                                GameObject wallUp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallUp.transform.rotation = Quaternion.Euler(0, 0, 0);
                                wallUp.transform.position = new Vector3(i*wallWidth+wallWidth/2, 0, j*wallWidth+wallWidth/2) + wallUp.transform.forward * wallWidth/2;
                                wallUp.transform.localScale =new Vector3(wallWidth+0.05f, wallHeight, 0.05f);
                                wallUp.transform.parent = grid.transform;
                                wallUp.GetComponent<Renderer>().material = fenceMaterial;
                        }

                        if (cell.wallRight == true) {
                                GameObject wallRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallRight.transform.rotation = Quaternion.Euler(0, 90, 0);
                                wallRight.transform.position = new Vector3(i*wallWidth+wallWidth/2, 0, j*wallWidth+wallWidth/2) + wallRight.transform.forward * wallWidth/2;
                                wallRight.transform.localScale =new Vector3(wallWidth+0.05f, wallHeight, 0.05f);
                                wallRight.transform.parent = grid.transform;
                                wallRight.GetComponent<Renderer>().material = fenceMaterial;
                        }

                        if (cell.wallDown == true) {
                                GameObject wallDown = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallDown.transform.rotation = Quaternion.Euler(0, 180, 0);
                                wallDown.transform.position = new Vector3(i*wallWidth+wallWidth/2, 0, j*wallWidth+wallWidth/2) + wallDown.transform.forward * wallWidth/2;
                                wallDown.transform.localScale =new Vector3(wallWidth+0.05f, wallHeight, 0.05f);
                                wallDown.transform.parent = grid.transform;
                                wallDown.GetComponent<Renderer>().material = fenceMaterial;
                        }

                        if (cell.wallLeft == true) {
                                GameObject wallLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallLeft.transform.rotation = Quaternion.Euler(0, 270, 0);
                                wallLeft.transform.position = new Vector3(i*wallWidth+wallWidth/2, 0, j*wallWidth+wallWidth/2) + wallLeft.transform.forward * wallWidth/2;
                                wallLeft.transform.localScale =new Vector3(wallWidth+0.05f, wallHeight, 0.05f);
                                wallLeft.transform.parent = grid.transform;
                                wallLeft.GetComponent<Renderer>().material = fenceMaterial;
                        }


                        // emissive lines
                        if (cell.wallUp == true) {
                                GameObject wallUp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallUp.transform.rotation = Quaternion.Euler(0, 0, 0);
                                wallUp.transform.position = new Vector3(i*wallWidth+wallWidth/2,  -wallHeight/2+0.1f, j*wallWidth+wallWidth/2) + wallUp.transform.forward * wallWidth/2*0.98f;
                                wallUp.transform.localScale =new Vector3(wallWidth, 0.1f, 0.05f);
                                wallUp.transform.parent = grid.transform;
                                wallUp.GetComponent<Renderer>().material = linesFenceMaterial;
                        }

                        if (cell.wallRight == true) {
                                GameObject wallRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallRight.transform.rotation = Quaternion.Euler(0, 90, 0);
                                wallRight.transform.position = new Vector3(i*wallWidth+wallWidth/2,  -wallHeight/2+0.1f, j*wallWidth+wallWidth/2) + wallRight.transform.forward * wallWidth/2 *0.98f;
                                wallRight.transform.localScale =new Vector3(wallWidth, 0.1f, 0.05f);
                                wallRight.transform.parent = grid.transform;
                                wallRight.GetComponent<Renderer>().material = linesFenceMaterial;
                        }

                        if (cell.wallDown == true) {
                                GameObject wallDown = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallDown.transform.rotation = Quaternion.Euler(0, 180, 0);
                                wallDown.transform.position = new Vector3(i*wallWidth+wallWidth/2,  -wallHeight/2+0.1f, j*wallWidth+wallWidth/2) + wallDown.transform.forward * wallWidth/2*0.98f;
                                wallDown.transform.localScale =new Vector3(wallWidth, 0.1f, 0.05f);
                                wallDown.transform.parent = grid.transform;
                                wallDown.GetComponent<Renderer>().material = linesFenceMaterial;
                        }

                        if (cell.wallLeft == true) {
                                GameObject wallLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                wallLeft.transform.rotation = Quaternion.Euler(0, 270, 0);
                                wallLeft.transform.position = new Vector3(i*wallWidth+wallWidth/2,  -wallHeight/2+0.1f, j*wallWidth+wallWidth/2) + wallLeft.transform.forward * wallWidth/2*0.98f;
                                wallLeft.transform.localScale =new Vector3(wallWidth, 0.1f, 0.05f);
                                wallLeft.transform.parent = grid.transform;
                                wallLeft.GetComponent<Renderer>().material = linesFenceMaterial;
                        }
                }
        }
        grid.transform.Translate(new Vector3(-size.x/2, 2.5f, -size.z/2));
}
}
