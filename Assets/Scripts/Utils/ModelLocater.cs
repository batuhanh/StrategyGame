using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace StrategyGame.Utils
{
    public class ModelLocator<IItem>
    {
        public class AddItemCompletedUnityEvent : UnityEvent<IItem> { }

        public readonly AddItemCompletedUnityEvent OnAddItemCompleted = new AddItemCompletedUnityEvent();

        private List<IItem> _items = new List<IItem>();

        public void AddItem(IItem item)
        {
            if (HasItem<IItem>())
            {
                throw new Exception("AddItem() failed. Item already added. Call HasItem<T>() first.");
            }
            _items.Add(item);
            OnAddItemCompleted.Invoke(item);
        }

        public bool HasItem<SubType>() where SubType : IItem
        {
            return GetItem<SubType>() != null;
        }

        public SubType GetItem<SubType>() where SubType : IItem
        {
            return _items.OfType<SubType>().ToList().FirstOrDefault<SubType>();
        }

        public IItem GetItem(Type type)
        {
            return _items.FirstOrDefault(item => item.GetType() == type);
        }

        public void RemoveItem(IItem item)
        {
            if (!HasItem<IItem>())
            {
                throw new Exception("RemoveItem() failed. Must call HasItem<T>() first.");
            }
            _items.Remove(item);
        }
    }
}
