using StrategyGame.MVC.Models;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Views
{
    public class GameGridView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public GameObject RowPrefab { get { return rowPrefab; } }
        public GameObject CellPrefab { get { return cellPrefab; } }
        public Transform RowsParent { get { return rowsParent; } }
        public Context Context { get { return _context; } }
        private bool _isInitialized = false;
        private Context _context;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform rowsParent;
        public void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
            }
        }
    }
}
