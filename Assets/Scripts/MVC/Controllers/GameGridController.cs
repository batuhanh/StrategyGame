using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public class GameGridController : BaseController<GameGridModel, GameGridView>
    {

        public GameGridController(GameGridModel model, GameGridView view) : base(model, view)
        {

        }
        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                Context.CommandManager.AddCommandListener<GameGridUpdateClickedCommand>(CreateRows);
            }
        }
        public bool SnapToGrid()
        {

            return true;
        }
        public Vector3 CheckBuildingPosSnapable(Building building, Vector3 offset)
        {
            float remainderX = (building.Blocks[0].transform.position + offset).x % (_model.CellSpacing / 2f);
            float remainderY = (building.Blocks[0].transform.position + offset).y % (_model.CellSpacing / 2f);
            Vector3 diff = new Vector3(remainderX,remainderY,0);
            float threshold = 0.1f;
            for (int i = 0; i < building.Blocks.Length; i++)
            {
                remainderX = (building.Blocks[i].transform.position + offset).x % (_model.CellSpacing / 2f);
                remainderY = (building.Blocks[i].transform.position + offset).y % (_model.CellSpacing / 2f);
                if ((remainderX > threshold || remainderX < (_model.CellSpacing / 2f) - threshold) &&
                    (remainderY > threshold || remainderY < (_model.CellSpacing / 2f) - threshold))
                {
                    return Vector3.zero;
                }
            }
            return diff;
        }
        public void CreateRows(GameGridUpdateClickedCommand gameGridUpdateClickedCommand)
        {
            float cellSize = 0.35f;
            Vector3 startCornerPos = new Vector3((-(_model.ColumnCount * _model.CellSpacing) / 2f) - (_model.CellSpacing - cellSize)
                , ((_model.RowCount * _model.CellSpacing) / 2f) - (_model.CellSpacing - cellSize), 0);
            for (int i = 0; i < _model.ColumnCount; i++)
            {
                GameObject row = PrefabUtility.InstantiatePrefab(_view.RowPrefab, _view.RowsParent) as GameObject;
                for (int j = 0; j < _model.RowCount; j++)
                {
                    Vector3 spawnLocalPos = new Vector3(j * _model.CellSpacing, 0, 0);
                    GameObject cell = PrefabUtility.InstantiatePrefab(_view.CellPrefab, row.transform) as GameObject;
                    cell.transform.SetParent(row.transform);
                    cell.transform.localPosition = spawnLocalPos;
                }
                row.transform.localPosition = startCornerPos + new Vector3(0, -i * _model.CellSpacing, 0);
            }
        }
    }
}
