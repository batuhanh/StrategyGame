using StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public abstract class BaseController<TModel, TView> 
    {

        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }

        private bool _isInitialized = false;
        protected readonly TModel _model;
        protected readonly TView _view;
        private Context _context;

        public BaseController(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }

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
