using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Models
{
    public class GameGridModel : BaseModel
    {
        public Observable<int> columnCount { get { return _columnCount; } }
        public Observable<int> rowCount { get { return _rowCount; } }

        private readonly Observable<int> _columnCount = new Observable<int>();
        private readonly Observable<int> _rowCount = new Observable<int>();
        

        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

            }
        }
    }
}
