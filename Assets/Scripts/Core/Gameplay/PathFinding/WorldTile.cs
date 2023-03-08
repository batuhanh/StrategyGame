using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.PathFinding
{
    public class WorldTile : MonoBehaviour
    {
        public int gCost;
        public int hCost;
        public int gridX, gridY, cellX, cellY;
        public bool walkable = true;
        public List<WorldTile> myNeighbours;
        public WorldTile parent;

        public WorldTile(bool _walkable, int _gridX, int _gridY)
        {
            walkable = _walkable;
            gridX = _gridX;
            gridY = _gridY;
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