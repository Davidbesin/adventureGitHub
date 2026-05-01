using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //Returns List. Accept Parameters startGrid and endGrid
    public List<GridTile> FindPath(GridTile startGridTile, GridTile targetGridTile)
    {
        List<GridTile> openList = new List<GridTile>(); //

        HashSet<GridTile> closedList = new HashSet<GridTile>(); // all grids that you have passed

        startGridTile.gCost = 0;
        startGridTile.hCost = GetDistance(startGridTile, targetGridTile);

        openList.Add(startGridTile); //Start with the starting grid here

        //one process
        while (openList.Count > 0)  //So keep searching??? Right now theres no movement. all in the logical side
        {
            GridTile currentGridTile = openList[0]; //Always a new tile
 
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentGridTile.fCost || openList[i].fCost == currentGridTile.fCost && openList[i].hCost < currentGridTile.hCost)
                {
                    currentGridTile = openList[i]; //MOveForward logically to the least costly
                }
            }

            openList.Remove(currentGridTile); //Moved on. 
            closedList.Add(currentGridTile); //Added to the Old winners lLIST.  
            // // so yea.what this does is to create two list. one called open, one called closed. 
            // open starts from the tile and keep going calculating. Then close follows behind to mark all the steps. 
            // this lowkey based on the f = g +h. i dunno how g inncreases though


            //Once youve found the path
            if (currentGridTile == targetGridTile)
            {
                return RetracePath(startGridTile, targetGridTile); // should be to know the path.
            }

            //Another process. should be for neighbours. check what should go to open list
            foreach (GridTile neighbour in currentGridTile.neighbours)
            {
                if (!neighbour.walkable || closedList.Contains(neighbour))
                    continue;

                int newCost = currentGridTile.gCost + GetDistance(currentGridTile, neighbour); //check for cost

                if (newCost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetGridTile);
                    neighbour.parent = currentGridTile;

                    if (!openList.Contains(neighbour))
                        openList.Add(neighbour);
                }
            }
        }

        return null;
    }

    List<GridTile> RetracePath (GridTile startGridTile, GridTile endGridTile)
    {
        List<GridTile> path = new List<GridTile>();
        GridTile currentGridTile = endGridTile;

        while (currentGridTile != startGridTile)
        {
            path.Add(currentGridTile);
            currentGridTile = currentGridTile.parent;
        }

        path.Reverse();
        return path;
    }

    //Heuristics
    int GetDistance(GridTile a, GridTile b)
    {
        int dstX = Mathf.Abs((int)a.transform.position.x - (int)b.transform.position.x);
        int dstY = Mathf.Abs((int)a.transform.position.z - (int)b.transform.position.z);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}