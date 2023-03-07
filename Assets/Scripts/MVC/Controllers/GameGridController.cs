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
        public void CreateRows(GameGridUpdateClickedCommand gameGridUpdateClickedCommand)
        {
            float cellSize = 0.35f;
            Vector3 startCornerPos = new Vector3((-(_model.ColumnCount * _model.CellSpacing) / 2f) - (_model.CellSpacing - cellSize)
                , ((_model.RowCount * _model.CellSpacing) / 2f) - (_model.CellSpacing - cellSize), 0);
            for (int i = 0; i < _model.ColumnCount; i++)
            {
                GameObject row = PrefabUtility.InstantiatePrefab(_view.RowPrefab, _view.RowsParent) as GameObject;
                //GameObject row = GameObject.Instantiate(_view.RowPrefab, _view.RowsParent);
                for (int j = 0; j < _model.RowCount; j++)
                {
                    Vector3 spawnLocalPos = new Vector3(j * _model.CellSpacing, 0, 0);
                    GameObject cell = PrefabUtility.InstantiatePrefab(_view.CellPrefab) as GameObject;
                    cell.transform.SetParent(row.transform);
                    cell.transform.localPosition = spawnLocalPos;
                }
                row.transform.localPosition = startCornerPos + new Vector3(0, -i * _model.CellSpacing, 0);
            }
        }
    }
}
