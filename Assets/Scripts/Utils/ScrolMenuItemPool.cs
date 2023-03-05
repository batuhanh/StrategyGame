﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StrategyGame.Utils
{
    public class ScrolMenuItemPool : ObjectPool 
    {
        public ScrolMenuItemPool(GameObject prefab, int size, Transform parent)
            : base(prefab, size, parent)
        {
           
        }
        public override GameObject GetObject()
        {
            return base.GetObject();

        }
        public override void ReleaseObject(GameObject obj)
        {
            base.ReleaseObject(obj);
        }
    }
}