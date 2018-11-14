using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    // Static instance
    public static Grid instance;

    // Public Variables
    public bool canPlace;
    public bool initialPositionValid;
    public Vector3 position;

    // Private Variables
    private Cell[] cells;
    private Vector3 lastPosition;
    private List<Cell> cellsSelected = new List<Cell>();

    private void Awake()
    {
        // Initiate
        instance = this;
        cells = GetComponentsInChildren<Cell>();
    }

    void Update()
    {
        // Toggle based on edit mode
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].gameObject.SetActive(BuildManager.instance.editMode);
        }

        if (BuildManager.instance.editMode == false)
        {
            canPlace = false;
            initialPositionValid = false;
        }
        // Check that turret to build isnt null
        if (BuildManager.instance.turretToBuild == null)
            return;

        // Set position
        List<Cell> cellList = Grid.instance.GetPosition(BuildManager.instance.turretToBuild.radius);

        // Get avg position
        position = Vector3.zero;
        for (int i = 0; i < cellList.Count; i++)
        {
            position += cellList[i].transform.position;

        }
        position /= cellList.Count;

        // Check that the cell list is the appropriate size
        if (position != lastPosition)
        {
            // Check the count is equal to the amount we need
            if (cellList.Count == (BuildManager.instance.turretToBuild.radius * BuildManager.instance.turretToBuild.radius))
            {
                cellsSelected = cellList;

                lastPosition = position;
                canPlace = true;
                initialPositionValid = true;

                for (int i = 0; i < cells.Length; i++)
                {
                    if (cellList.Contains(cells[i]))
                        cells[i].SetStatus(Cell.Status.selected);
                    else
                        cells[i].SetStatus(Cell.Status.unselected);
                }
            }
            else
            {
                // Set can place to false
                canPlace = false;
            }
        }
    }

    public Cell GetClosestCell(Vector3 position, bool world = false)
    {
        // Temporary variable cell to return
        Cell closestCell = null;

        // A distance parameter to compare cells
        float closestDist = int.MaxValue;
        // Loop through each cell to find closest one
        for (int i = 0; i < cells.Length; i++)
        {
            // Calculate distance from Cell at index i, to position
            Vector3 positionToCheck = world ? cells[i].transform.position : Camera.main.WorldToScreenPoint(cells[i].transform.position);
            Vector3 difference = positionToCheck - position;
            float dist = difference.magnitude;

            // Check if the dist is smaller than the closest distance
            if (dist <= closestDist)
            {
                // We found the next closest cell
                closestCell = cells[i];
                closestDist = dist;
            }
        }

        return closestCell;
    }

    public List<Cell> GetPosition(int radius)
    {
        Vector3 cellPosition = GetClosestCell(Input.mousePosition).transform.position;

        List<Cell> cells = new List<Cell>();
        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                Cell cellToAdd = GetClosestCell(cellPosition + (Vector3.forward * y) + (Vector3.right * x), true);
                if (cells.Count > 0)
                    if (cells[0].transform.parent != cellToAdd.transform.parent)
                        continue;
                if (cells.Contains(cellToAdd))
                    continue;
                if (cellToAdd.IsUsed())
                    continue;
                cells.Add(cellToAdd);
            }
        }

        return cells;
    }

    public void Place()
    {
        // Set cells as taken
        for(int i = 0; i < cellsSelected.Count; i++)
        {
            cellsSelected[i].SetStatus(Cell.Status.used);
        }

        BuildManager.instance.turretToBuild.cellsTaken = cellsSelected;
    }
}
