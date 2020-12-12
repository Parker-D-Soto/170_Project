using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
//using Subtegral.DialogueSystem.DataContainers;

public class Dialogue_Graph : EditorWindow
{
    private Dialogue_Graphview _graphView;
    private string _fileName = "New Narrative";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<Dialogue_Graph>();
        window.titleContent = new GUIContent(text: "Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(new Rect(10, 30, 200, 140));
        _graphView.Add(miniMap);
    }

    private void ConstructGraphView()
    {
        _graphView = new Dialogue_Graphview
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);


    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField(label: "File Name: ");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt =>  _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(save: true)) { text = "Save Data" });
        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(save: false)) { text = "Load Data" });


        var nodeCreateButton = new Button(clickEvent: () => { _graphView.CreateNode("Dialogue Node");});
        nodeCreateButton.text = "Create Node";

        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog(title: "Invalid file name!", message: "Please enter a valid file name.", ok: "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if (save)
        {
            saveUtility.SaveGraph(_fileName);
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
    }

    private void LoadData()
    {
        throw new NotImplementedException();
    }

    private void SaveData()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
