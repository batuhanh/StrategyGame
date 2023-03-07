using UnityEditor;
using StrategyGame.MVC.Views;
using UnityEngine;

[CustomEditor(typeof(GameGridView))]
public class GameGridViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameGridView gameGridView = (GameGridView)target;

        if (GUILayout.Button("Update Grid On Runtime"))
        {
            gameGridView.Context.CommandManager.ExecuteCommand(new GameGridUpdateClickedCommand());
        }
    }
}

