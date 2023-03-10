using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC;
using StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StrategyGame.Core.Managers
{
    public class InputManager : MonoBehaviour
    {
        public MouseState CurrentMouseState { get { return _currentLeftMouseState; } }
        public Vector3 MousePostition { get { return _mousePostition; } }

        private MouseState _currentLeftMouseState;
        private Vector3 _mousePostition;

        public static event Action<int,Vector3> OnMouseDownEvent;
        public static event Action<int,Vector3> OnMouseUpEvent;
        public static InputManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        private void Update()
        {
            _mousePostition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                _currentLeftMouseState = MouseState.Down;
                OnMouseDownEvent?.Invoke(0, _mousePostition);
            }
            else if (Input.GetMouseButton(0))
            {
                _currentLeftMouseState = MouseState.Hold;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _currentLeftMouseState = MouseState.Up;
                OnMouseUpEvent?.Invoke(0, _mousePostition);
            }
            else
            {
                _currentLeftMouseState = MouseState.None;
            }
            

            if (Input.GetMouseButtonDown(1))
            {
                OnMouseDownEvent?.Invoke(1, _mousePostition);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                OnMouseUpEvent?.Invoke(1, _mousePostition);
            }
        }
    }
    public enum MouseState
    {
        None,
        Down,
        Hold,
        Up
    }
}
