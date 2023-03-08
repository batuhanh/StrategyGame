using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.Core.Managers;
using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StrategyGame.MVC.Controllers
{
    public class GameGridController : BaseController<GameGridModel, GameGridView>
    {
        private Vector3 offset;
        public GameGridController(GameGridModel model, GameGridView view) : base(model, view)
        {

        }
        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
            }
        }
        public void UpdatOffset(Vector3 pos)
        {
            offset = pos - GetMouseWorldPosition();
        }
        public void CheckHoldedObject()
        {

            if (_view.objToPlace && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePostition) + offset;
                _view.objToPlace.gameObject.transform.position = SnapCoordinateToGrid(pos);
                if (InputManager.Instance.CurrentMouseState == MouseState.Up)
                {
                    _view.objToPlace.GetColliderVertexPositionsLocal();
                    _view.objToPlace.CalculateSizeInCells();
                    if (CanBePlaced(_view.objToPlace))
                    {
                        Building objToPlace = _view.objToPlace;
                        objToPlace.Place();
                        Vector3Int start = _view.GridLayout.WorldToCell(objToPlace.GetStartPosition());
                        TakeArea(start, objToPlace.Size);
                        _view.objToPlace = null;
                    }
                }
            }
        }
        public static Vector3 GetMouseWorldPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                return hit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
        public Vector3 SnapCoordinateToGrid(Vector3 position)
        {
            Vector3Int cellPos = _view.GridLayout.WorldToCell(position);
            position = _view.Grid.GetCellCenterWorld(cellPos);
            return position;
        }

        private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tileMap)
        {
            TileBase[] tileBases = new TileBase[area.size.x * area.size.y * area.size.z];
            int counter = 0;
            foreach (var v in area.allPositionsWithin)
            {
                Vector3Int pos = new Vector3Int(v.x, v.y, 0);
                tileBases[counter] = tileMap.GetTile(pos);
                counter++;
            }
            return tileBases;
        }
        public bool CanBePlaced(Building building)
        {
            BoundsInt area = new BoundsInt();
            area.position = _view.GridLayout.WorldToCell(_view.objToPlace.GetStartPosition());
            area.size = _view.objToPlace.Size;

            TileBase[] baseArray = GetTilesBlock(area, _view.TileMap);

            foreach (var b in baseArray)
            {
                if (b == _view.BuildingTile)
                {
                    return false;
                }
            }
            return true;
        }
        public void TakeArea(Vector3Int start, Vector3Int size)
        {
            _view.TileMap.BoxFill(start, _view.BuildingTile, start.x, start.y, start.x + size.x, start.y + size.y);

        }

    }
}
