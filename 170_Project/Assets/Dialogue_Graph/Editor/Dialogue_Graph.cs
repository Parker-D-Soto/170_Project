﻿using System;
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
        GenerateBlackBoard();
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
        blackboard.addItemRequested = _blackboard => { _graphView.AddPropertyToBlackBoard(ExposedProperty.CreateInstance(), false); };
        blackboard.editTextRequested = (blackboard1, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField) element).text;
            if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog("Error", "This property name already exists, please choose another one!", "OK");
                return;
            }

            var propertyIndex = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            _graphView.ExposedProperties[propertyIndex].PropertyName = newValue;
            ((BlackboardField) element).text = newValue;
        };
        blackboard.SetPosition(new Rect(10, 30, 200, 300));
        _graphView.Add(blackboard);
        _graphView.Blackboard = blackboard;
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };

        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));

        miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
        _graphView.Add(miniMap);
    }

    private void ConstructGraphView()
    {
        _graphView = new Dialogue_Graphview(this)
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

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
