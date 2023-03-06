using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StrategyGame.Utils
{
    public class ScrolMenuItemPool : ObjectPool 
    {
        private Transform _disabledParent;
        private Transform _content;
        public ScrolMenuItemPool(GameObject[] prefabs, int size, Transform disabledParent, Transform content)
            : base(prefabs, size + 20, disabledParent)
        {
            //20 is extra item count to make randomless scroll view
            _disabledParent = disabledParent;
            _content = content;
        }
        public override GameObject GetObject()
        {
            GameObject founded = base.GetObject();
            founded.transform.SetParent(_content);
            return founded;

        }
        public override void ReleaseObject(GameObject obj)
        {

            int oldIndex = pool.IndexOf(obj);
            int newIndex = pool.Count - 2;
            GameObject item = pool[oldIndex];
            pool.RemoveAt(oldIndex);
            pool.Insert(newIndex, item);
            item.transform.SetParent(_disabledParent);
            base.ReleaseObject(obj);
        }
    }
}
