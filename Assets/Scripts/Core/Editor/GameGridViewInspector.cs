using UnityEditor;
using StrategyGame.MVC.Views;
using UnityEngine;

[CustomEditor(typeof(GameGridView))]
public class GameGridViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //GameGridViewInspector gameGridViewInspector = (GameGridViewInspector)target;

        if (GUILayout.Button("Update Grid On Editor"))
        {

        }
    }
}

