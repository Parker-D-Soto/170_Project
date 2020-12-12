using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Subtegral.DialogueSystem.DataContainers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class Dialogue_Graphview : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public Dialogue_Graphview()
    {
        styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(path: "DialogueGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();


        AddElement(GenerateEntryPointNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(funcCall: (port) => 
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    private Port GeneratePort(Dialogue_Node node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float)); //arbitrary type
    }

    private Dialogue_Node GenerateEntryPointNode()
    {
        var node = new Dialogue_Node
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "ROOT (No Text here)",
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100,y: 200, width:100, height: 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public Dialogue_Node CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new Dialogue_Node()
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);


        var button = new Button(clickEvent: () => { AddChoicePort(dialogueNode); });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.DialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(textField);

        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(position: Vector2.zero, defaultNodeSize));

        return dialogueNode;
    }

    public void AddChoicePort(Dialogue_Node dialogueNode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>(name: "type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogueNode.outputContainer.Query(name: "connector").ToList().Count;
        var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice {outputPortCount+1}":overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };

        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);

        generatedPort.contentContainer.Add(child: new Label(text: "   "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(()=>RemovePort(dialogueNode,generatedPort))
        {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);

        generatedPort.portName = choicePortName;
        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
        
    }

    private void RemovePort(Dialogue_Node dialogueNode, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        dialogueNode.outputContainer.Remove(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}
