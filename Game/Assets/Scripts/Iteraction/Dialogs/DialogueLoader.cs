using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.IO;
using UnityEngine;

namespace Dialogs
{
    public static class DialogueLoader
    {
        #region Methods
        public static DialogueNode[] FromJson(string path, Dictionary<int, Action> dialogueActions = null)
        {
            RawDialogueNode[] nodes;
            JsonData json;
            try
            {
                /*
                var text = File.ReadAllText(
                    Path.Combine(Application.dataPath + "/StreamingAssets/Dialogues", path)
                    );
                */
                var text = Resources.Load<TextAsset>(
                    Path.Combine("Dialogues",
                    Path.Combine(
                        Path.GetDirectoryName(path),
                        Path.GetFileNameWithoutExtension(path))
                        )
                    ).text;


                if (string.IsNullOrEmpty(text))
                {
                    Debug.LogError("Dialog is Empty");
                    return null;
                }

                json = JsonMapper.ToObject(text);
                nodes = ReadNodes(json);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
            return BuildTree(
                nodes,
                dialogueActions
                );
        }

        private static RawDialogueNode[] ReadNodes(JsonData data)
        {
            RawDialogueNode[] dialogNodes = new RawDialogueNode[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var node = new RawDialogueNode();
                node.Id = (int)data[i]["Id"];
                node.Title = (string)data[i]["Title"];
                node.FirstMemberText = (string)data[i]["FirstMemberText"];
                node.SecondMemberText = (string)data[i]["SecondMemberText"];
                node.Children = ReadChildren(data[i]["Children"]);
                dialogNodes[i] = node;
            }
            return dialogNodes;
        }

        private static int[] ReadChildren(JsonData data)
        {
            int[] array = new int[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                array[i] = (int)data[i];
            }
            return array;
        }

        private static DialogueNode[] BuildTree(RawDialogueNode[] rawDialogNodes, Dictionary<int, Action> dialogueActions)
        {
            if (rawDialogNodes == null)
            {
                return null;
            }
            DialogueNode[] dialogueNodes = CreateDialogueNodes(
                rawDialogNodes,
                dialogueActions
                );
            return dialogueNodes;
        }

        private static DialogueNode[] CreateDialogueNodes(RawDialogueNode[] rawDialogNodes, Dictionary<int, Action> dialogueActions)
        {
            DialogueNode[] dialogueNodes = new DialogueNode[rawDialogNodes.Length];
            DialogueNode node;
            RawDialogueNode rawNode;
            for (int i = 0; i < rawDialogNodes.Length; i++)
            {
                rawNode = rawDialogNodes[i];
                node = new DialogueNode(
                    rawNode.Id,
                    rawNode.Title,
                    rawNode.FirstMemberText,
                    rawNode.SecondMemberText
                    );
                if (dialogueActions != null)
                {

                    Action action;
                    dialogueActions.TryGetValue(rawNode.Id, out action);
                    node.SetAction(action);
                }
                dialogueNodes[i] = node;
            }
            ConnectNodes(dialogueNodes, rawDialogNodes);
            return dialogueNodes;
        }

        private static void ConnectNodes(DialogueNode[] dialogueNodes, RawDialogueNode[] rawDialogueNodes)
        {
            DialogueNode node;
            RawDialogueNode rawNode;
            for (int i = 0; i < dialogueNodes.Length; i++)
            {
                rawNode = rawDialogueNodes[i];
                node = dialogueNodes[i];
                for (int j = 0; j < rawNode.Children.Length; j++)
                {
                    var child = dialogueNodes
                        .Where(element => element.Id == rawNode.Children[j])
                        .SingleOrDefault();
                    if (node != null)
                    {
                        node.AddChildren(child);
                    }
                    else
                    {
                        Debug.LogWarning("Dialog node is not found");
                    }
                }
            }
        }


        #endregion //Methods
    }
}