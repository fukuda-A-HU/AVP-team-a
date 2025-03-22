using UnityEngine;

public class EdgeView : MonoBehaviour
{
    private NodeView prevNode;
    private NodeView nextNode;

    public void Set(NodeView prevNode, NodeView nextNode)
    {
        this.prevNode = prevNode;
        this.nextNode = nextNode;
    }
}
