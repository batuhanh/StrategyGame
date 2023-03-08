using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC.Models;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StrategyGame.MVC.Views
{
    public class GameGridView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        public Renderer GridBgRenderer { get { return _gridBgRenderer; } }
        public GridLayout GridLayout { get { return _gridLayout; } }
        public Grid Grid { get { return _grid; } }
        public Tilemap TileMap { get { return _tileMap; } }
        public TileBase BuildingTile { get { return _buildingTile; } }
        public TileBase SoldierTile { get { return _soldierTile; } }
        public TileBase EmptyTile { get { return _emptyTile; } }
        private bool _isInitialized = false;
        private Context _context;
        [SerializeField] private Renderer _gridBgRenderer;
        [SerializeField] private GridLayout _gridLayout;
        [SerializeField] private Grid _grid;
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private TileBase _buildingTile;
        [SerializeField] private TileBase _soldierTile;
        [SerializeField] private TileBase _emptyTile;

        public Building objToPlace;
        public void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
            }
        }
        public void Update()
        {
            GameGrid.Instance.GameGridController.CheckHoldedObject();
        }

    }
}
