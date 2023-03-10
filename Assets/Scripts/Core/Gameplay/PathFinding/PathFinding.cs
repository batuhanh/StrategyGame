using StrategyGame.MVC.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame.Core.Gameplay.PathFinding
{
    public class PathFinding : MonoBehaviour
    {
       /* void FindPath(Vector3 startPosition, Vector3 endPosition)
        {
            WorldTile startNode = GetWorldTileByCellPosition(startPosition);
            WorldTile targetNode = GetWorldTileByCellPosition(endPosition);

            List<WorldTile> openSet = new List<WorldTile>();
            HashSet<WorldTile> closedSet = new HashSet<WorldTile>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                WorldTile currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (WorldTile neighbour in currentNode.myNeighbours)
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }*/
    }
}
