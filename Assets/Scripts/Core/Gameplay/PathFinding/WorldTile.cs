using StrategyGame.MVC;
using StrategyGame.MVC.Controllers;
using StrategyGame.MVC.Views;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace StrategyGame.Core.Gameplay.PathFinding
{
    public class WorldTile
    {
        public int gCost;
        public int hCost;
        public int gridX, gridY, gridZ, cellX, cellY;
        public bool walkable = true;
        public List<WorldTile> myNeighbours;
        public WorldTile parent;

        public WorldTile(bool _walkable, int _gridX, int _gridY, int _gridZ, bool canCreateNeigh)
        {
            walkable = _walkable;
            gridX = _gridX;
            gridY = _gridY;
            gridZ = _gridZ;
            if (!PathFinder.Instance.allTiles.ContainsKey(new Vector3Int(gridX, gridY, gridZ)))
            {
                PathFinder.Instance.allTiles.Add(new Vector3Int(gridX, gridY, gridZ), this);
            }
            if (canCreateNeigh)
            {
                CreateNeighbours();
            }
        }
        public Vector3Int GetGridPos()
        {
            return new Vector3Int(gridX, gridY, gridZ);
        }
        public void CreateNeighbours()
        {
            myNeighbours = new List<WorldTile>();

            AddNeighbour(new Vector3Int(gridX - 1, gridY - 1, gridZ));
            AddNeighbour(new Vector3Int(gridX - 1, gridY, gridZ));
            AddNeighbour(new Vector3Int(gridX - 1, gridY + 1, gridZ));
            AddNeighbour(new Vector3Int(gridX, gridY + 1, gridZ));
            AddNeighbour(new Vector3Int(gridX + 1, gridY + 1, gridZ));
            AddNeighbour(new Vector3Int(gridX + 1, gridY, gridZ));
            AddNeighbour(new Vector3Int(gridX + 1, gridY - 1, gridZ));
            AddNeighbour(new Vector3Int(gridX, gridY - 1, gridZ));

        }
        private void AddNeighbour(Vector3Int targetCellPos)
        {
            WorldTile curNeigh;
            if (PathFinder.Instance.allTiles.ContainsKey(targetCellPos))
            {
                curNeigh = PathFinder.Instance.allTiles[targetCellPos];
            }
            else
            {
                curNeigh = new WorldTile(IsTileWalkable(targetCellPos), targetCellPos.x, targetCellPos.y, targetCellPos.z, false);
            }
            
            myNeighbours.Add(curNeigh);

        }
        private bool IsTileWalkable(Vector3Int targetCellPos)
        {
            GameGridView gameGridView = GameGrid.Instance.GameGridView;
            if (gameGridView.TileMap.GetTile(targetCellPos) == gameGridView.EmptyTile
                || gameGridView.TileMap.GetTile(targetCellPos) == gameGridView.SoldierTile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
