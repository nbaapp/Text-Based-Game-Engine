using Unity.VisualScripting;
using UnityEngine;
using XNode;

public class StoryInterpreter : MonoBehaviour
{
    public StoryGraph currentGraph;
    private StoryNode currentNode;

    private StoryNode savedNode;
    private bool exploringFuture;
    
    public void SaveGame(StoryNode node) {
        savedNode = node;
        exploringFuture = true;
    }

    public void EndFutureExploration() {
        exploringFuture = false;
        currentNode = savedNode;
        // Logic to switch to the return path branch
        ProgressToNextNode((savedNode as SavePointNode).NodeToReturnTo);
        savedNode = null;
    }

    private void ProgressToNextNode(StoryNode nextNode) {
        if (exploringFuture && currentNode is EndPointNode) {
            EndFutureExploration();
        }
        else if (nextNode == null) {
            Debug.LogError("Done with story!");
        } 
        else {
            currentNode = nextNode;
        }
        UpdateStory();
    }

    void Start()
    {
        // Start at the first node of the current graph
        currentNode = currentGraph.nodes[0] as StoryNode;
        UpdateStory();
    }

    public void UpdateStory()
    {
        if (currentNode != null)
        {
            // Display current node's story text and choices
            FindObjectOfType<TextDisplayManager>().UpdateText(currentNode.storyText);
            FindObjectOfType<ChoiceManager>().DisplayChoices(currentNode.choices);
        }
    }

    public void OnChoiceMade(int choiceIndex)
    {
        // Get the appropriate choice port
        NodePort choicePort = currentNode.GetOutputPort("choices " + choiceIndex);
        StoryNode nextNode = null;
        if (choicePort != null) {
            nextNode = choicePort.Connection.node as StoryNode;
        }

        if (currentNode is SavePointNode) {
            SaveGame(currentNode);

            // set next node to 'future path seen' node
            nextNode = (currentNode as SavePointNode).FuturePathSeenNode;

            ProgressToNextNode(nextNode);
        }
        else
        {
            // Move to the connected node within the same graph
            ProgressToNextNode(nextNode);
        }
        /*
        else if (currentNode is ZoneTransitionNode)
        {
            ZoneTransitionNode zoneNode = currentNode as ZoneTransitionNode;
            if (zoneNode.nextGraph != null && !string.IsNullOrEmpty(zoneNode.nextGraphNodeIdentifier)) 
            {
                // Transition to a node in a different graph
                TransitionToNewGraph(zoneNode.nextGraph, zoneNode.nextGraphNodeIdentifier);
            }
        }
        */
    }

    private void TransitionToNewGraph(StoryGraph newGraph, string nodeIdentifier)
    {
        currentGraph = newGraph;

        // Find the node in the new graph with the specified identifier
        foreach (Node node in newGraph.nodes)
        {
            if (node is StoryNode storyNode && storyNode.nodeIdentifier == nodeIdentifier)
            {
                currentNode = storyNode;
                break;
            }
        }
        
        UpdateStory();
    }
}
