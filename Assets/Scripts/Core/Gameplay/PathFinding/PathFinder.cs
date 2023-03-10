using StrategyGame.Core.Gameplay.SoldierSystem;
using StrategyGame.MVC;
using StrategyGame.MVC.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame.Core.Gameplay.PathFinding
{
    public class PathFinder : MonoBehaviour
    {
        public Dictionary<Vector3Int, WorldTile> allTiles = new Dictionary<Vector3Int, WorldTile>();
        public List<WorldTile> lastPath=new List<WorldTile>();
        public static PathFinder Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
          
        }
        public void FindPath(Vector3 startPosition, Vector3 endPosition)
        {
           
            lastPath.Clear();
            allTiles.Clear();
            Vector3Int startCell = GameGrid.Instance.GameGridView.GridLayout.WorldToCell(startPosition);
            Vector3Int targetCell = GameGrid.Instance.GameGridView.GridLayout.WorldToCell(endPosition);

            WorldTile startNode = new WorldTile(true, startCell.x, startCell.y, startCell.z, true);
            WorldTile targetNode = new WorldTile(true, targetCell.x, targetCell.y, targetCell.z, false);

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
                    
                    lastPath=RetracePath(startNode, targetNode);
                    return;
                }

                if (currentNode.myNeighbours == null)
                {
                    currentNode.CreateNeighbours();
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
        }
        public int GetDistance(WorldTile nodeA, WorldTile nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        public List<WorldTile> RetracePath(WorldTile startNode, WorldTile targetNode)
        {
            List<WorldTile> path = new List<WorldTile>();
            WorldTile currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }
    }
}
