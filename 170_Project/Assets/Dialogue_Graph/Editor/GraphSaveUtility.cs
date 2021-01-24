using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private Dialogue_Graphview _targetGraphView;

    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<Dialogue_Node> Nodes => _targetGraphView.nodes.ToList().Cast<Dialogue_Node>().ToList();

    public static GraphSaveUtility GetInstance(Dialogue_Graphview targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView

        };
    }

    public void SaveGraph(string fileName)
    {
        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        if (!SaveNodes(dialogueContainer)) return;
        SaveExposedProperties(dialogueContainer);
        //Auto creates folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
            AssetDatabase.CreateFolder(parentFolder: "Assets", newFolderName: "Resources");

        AssetDatabase.CreateAsset(dialogueContainer, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private void SaveExposedProperties(DialogueContainer dialogueContainer)
    {
        dialogueContainer.ExposedProperties.Clear();
        dialogueContainer.ExposedProperties.AddRange(_targetGraphView.ExposedProperties);
    }

    private bool SaveNodes(DialogueContainer dialogueContainer)
    {
        if (!Edges.Any()) return false; //no edges in graph

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as Dialogue_Node;
            var inputNode = connectedPorts[i].input.node as Dialogue_Node;

            dialogueContainer.NodeLinks.Add(item: new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                GUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position,
                Mutation = dialogueNode.Mutation,
                ExitPoint = dialogueNode.ExitPoint
            });
        }

        return true;
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);

        if(_containerCache == null)
        {
            EditorUtility.DisplayDialog(title: "File not found", message: "Target Dialogue graph file does not exist.", ok: "OK");
            return;
        }

        clearGraph();
        CreateNodes();
        ConnectNodes();
        CreateExposedProperties();
    }

    private void CreateExposedProperties()
    {
        _targetGraphView.ClearBlackBoardAndExposedProperties();
        foreach(var exposedProperty in _containerCache.ExposedProperties)
        {
            _targetGraphView.AddPropertyToBlackBoard(exposedProperty);
        }
    }

    private void clearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;


        foreach (var node in Nodes)
        {
            if (node.EntryPoint) continue;

            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));

            _targetGraphView.RemoveElement(node);
        }
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodeData)
        {
            if (string.IsNullOrEmpty(nodeData.Mutation))
            {
                var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText, Vector2.zero);
                tempNode.GUID = nodeData.GUID;
                _targetGraphView.AddElement(tempNode);

                var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.GUID).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
            }
            else
            {
                var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText, Vector2.zero, nodeData.Mutation);
                tempNode.GUID = nodeData.GUID;
                _targetGraphView.AddElement(tempNode);

                var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.GUID).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
            }
            
            
        }
    }

    private void ConnectNodes()
    {
        for (var i = 0; i < Nodes.Count(); i++)
        {
            var k = i;
            var connections = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[k].GUID).ToList();
            for (var j = 0; j < connections.Count(); j++)
            {

                var targetNodeGuid = connections[j].TargetNodeGuid;

                var targetNode = Nodes.SingleOrDefault(x => x.GUID == targetNodeGuid);

                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_containerCache.DialogueNodeData.First(x => x.GUID == targetNodeGuid).Position,_targetGraphView.defaultNodeSize));

            }
        }
    }

    private void LinkNodes(Port output_p, Port input_p)
    {

        var tempEdge = new Edge
        {
            output = output_p,
            input = input_p
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);

        _targetGraphView.Add(tempEdge);
    }




}
