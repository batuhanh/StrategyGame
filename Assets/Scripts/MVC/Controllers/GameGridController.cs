using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.Core.Gameplay.SoldierSystem;
using StrategyGame.Core.Managers;
using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
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
                _view.objToPlace.transform.position = (_view.objToPlace.transform.position - _view.objToPlace.GetStartPosition()) + SnapCoordinateToGrid(pos);
                if (CanBePlaced(_view.objToPlace))
                {
                    _view.objToPlace.SetColor(UnityEngine.Color.white);
                }
                else
                {
                    _view.objToPlace.SetColor(UnityEngine.Color.red);
                }
                if (InputManager.Instance.CurrentMouseState == MouseState.Up)
                {
                    _view.objToPlace.GetColliderVertexPositionsLocal();
                    _view.objToPlace.CalculateSizeInCells();
                    if (CanBePlaced(_view.objToPlace))
                    {
                        Building objToPlace = _view.objToPlace;
                        objToPlace.Place();
                        Vector3Int start = _view.GridLayout.WorldToCell(objToPlace.GetStartPosition());
                        TakeArea(start, objToPlace.Size, _view.BuildingTile);
                        _view.objToPlace = null;
                    }
                }
            }
        }
        public bool IsPosOnGrid(Vector3 position)
        {
            Vector3Int curTilePos = _view.GridLayout.WorldToCell(position);
            TileBase curTile = _view.TileMap.GetTile(curTilePos);

            return curTile != null;
        }
        public TileBase GetTileBase(Vector3 position)
        {
            Vector3Int curTilePos = _view.GridLayout.WorldToCell(position);
            return _view.TileMap.GetTile(curTilePos);
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
        public Vector3 PutSoldierToClosest(Soldier soldier, Vector3 position)
        {
            Vector3Int closestCell = FindClosestAvailableCell(position);
            ChangeGridTileState(closestCell, new Vector3Int(0, 0, 0), _view.SoldierTile);
            return _view.Grid.GetCellCenterWorld(closestCell);
        }
        public void ChangeGridTileState(Vector3 worldPos, Vector3Int size, TileBase tiletype)
        {
            Vector3Int cellPos = _view.GridLayout.WorldToCell(worldPos);
            TakeArea(cellPos, size, tiletype);
        }
        public void ChangeGridTileState(Vector3Int cellPos, Vector3Int size, TileBase tiletype)
        {
            TakeArea(cellPos, size, tiletype);
        }
        private Vector3Int FindClosestAvailableCell(Vector3 startPos)
        {
            int size = 4;
            int searchCount = 50;
            int currentSearch = 0;
            Vector3 curPos = startPos;
            while (currentSearch < searchCount)
            {
                curPos += new Vector3(-_view.GridLayout.cellSize.x, -_view.GridLayout.cellSize.y, 0);
                Vector3Int leftDownCornerIndex = _view.GridLayout.WorldToCell(curPos);
                TileBase currentTile = _view.TileMap.GetTile(leftDownCornerIndex);

                Vector3Int curIndex = leftDownCornerIndex;
                currentTile = _view.TileMap.GetTile(curIndex);
                for (int i = 0; i < size + 1; i++)
                {
                    if (currentTile == _view.EmptyTile)
                    {
                        return curIndex;
                    }

                    curPos += new Vector3(0, _view.GridLayout.cellSize.y, 0);
                    curIndex = _view.GridLayout.WorldToCell(curPos);
                    currentTile = _view.TileMap.GetTile(curIndex);
                    //Debug.DrawRay(curPos, curPos + new Vector3(0, 0, -20), UnityEngine.Color.yellow, 100f);
                }
                for (int i = 0; i < size + 1; i++)
                {
                    if (currentTile == _view.EmptyTile)
                    {
                        return curIndex;
                    }
                    curPos += new Vector3(_view.GridLayout.cellSize.x, 0, 0);
                    curIndex = _view.GridLayout.WorldToCell(curPos);
                    currentTile = _view.TileMap.GetTile(curIndex);
                    //Debug.DrawRay(curPos, curPos + new Vector3(0, 0, -20), UnityEngine.Color.red, 100f);
                }
                for (int i = 0; i < size + 1; i++)
                {
                    if (currentTile == _view.EmptyTile)
                    {
                        return curIndex;
                    }
                    curPos -= new Vector3(0, _view.GridLayout.cellSize.y, 0);
                    curIndex = _view.GridLayout.WorldToCell(curPos);
                    currentTile = _view.TileMap.GetTile(curIndex);
                    //Debug.DrawRay(curPos, curPos + new Vector3(0, 0, -20), UnityEngine.Color.blue, 100f);
                }
                for (int i = 0; i < size + 1; i++)
                {
                    if (currentTile == _view.EmptyTile)
                    {
                        return curIndex;
                    }
                    curPos -= new Vector3(_view.GridLayout.cellSize.x, 0, 0);
                    curIndex = _view.GridLayout.WorldToCell(curPos);
                    currentTile = _view.TileMap.GetTile(curIndex);
                    //Debug.DrawRay(curPos, curPos + new Vector3(0, 0, -20), UnityEngine.Color.green, 100f);
                }
                size += 2;
                currentSearch++;
            }

            return new Vector3Int();
        }
      
        public bool CanBePlaced(Building building)
        {
            BoundsInt area = new BoundsInt();
            area.position = _view.GridLayout.WorldToCell(_view.objToPlace.GetStartPosition());
            area.size = _view.objToPlace.Size + new Vector3Int(1, 1, 1);

            TileBase[] baseArray = GetTilesBlock(area, _view.TileMap);

            foreach (var b in baseArray)
            {
                if (b != _view.EmptyTile)
                {
                    return false;
                }
            }
            return true;
        }
        public void TakeArea(Vector3Int start, Vector3Int size, TileBase tileType)
        {
            _view.TileMap.BoxFill(start, tileType, start.x, start.y, start.x + size.x, start.y + size.y);

        }

    }
}
