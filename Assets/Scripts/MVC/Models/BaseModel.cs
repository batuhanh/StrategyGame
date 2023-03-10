using StrategyGame.Core.Managers;
using StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace StrategyGame.MVC.Models
{
    public abstract class BaseModel 
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }

        private bool _isInitialized = false;
        private Context _context;

        public virtual void Initialize(Context context)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                _context = context;

            }
        }

        public void RequireIsInitialized()
        {
            if (!_isInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
    }
}
