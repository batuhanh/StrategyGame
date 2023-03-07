using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Models
{
    public class GameGridModel : BaseModel
    {
        public int ColumnCount { get { return _columnCount; } }
        public int RowCount { get { return _rowCount; } }
        public float CellSpacing { get { return _cellSpacing; } }

        private readonly int _columnCount = 50;
        private readonly int _rowCount = 50;
        private readonly float _cellSpacing = 0.40f;


        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

            }
        }
    }
}
