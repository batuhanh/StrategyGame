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
        public MouseState CurrentMouseState { get { return _currentMouseState; } }
        public Vector3 MousePostition { get { return _mousePostition; } }

        private MouseState _currentMouseState;
        private Vector3 _mousePostition;
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
            if (Input.GetMouseButtonDown(0))
            {
                _currentMouseState = MouseState.Down;
            }
            else if (Input.GetMouseButton(0))
            {
                _currentMouseState = MouseState.Hold;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _currentMouseState = MouseState.Up;
            }
            else
            {
                _currentMouseState = MouseState.None;
            }
            _mousePostition = Input.mousePosition;
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
