

[NodeTint("#B79F24")]
public class ZoneTransitionNode : StoryNode
{
    // New properties for graph transitions
    public StoryGraph nextGraph;
    public string nextGraphNodeIdentifier; // This should match the name of the node in the new graph you want to transition to
}
