using R3;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EdgeView : MonoBehaviour
{
    public SerializableReactiveProperty<float> radius = new SerializableReactiveProperty<float>(0.1f); // 円柱の半径
    public SerializableReactiveProperty<float> heightScale = new SerializableReactiveProperty<float>(1f); // 高さのスケール

    [SerializeField] private NodeView node;
    [SerializeField] private NodeView parentNode;
    [SerializeField] private BranchExpander branchExpander;

    public void Start()
    {
        radius.Subscribe(x =>{
            Debug.Log($"radius: {x}");
            UpdateEdge();
        });

        heightScale.Subscribe(x =>{
            Debug.Log($"heightScale: {x}");
            UpdateEdge();
        });
    }

    public async UniTask Set(NodeView start, NodeView end)
    {
        node = start;
        parentNode = end;

        if (node == null || parentNode == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(parentNode.transform);

        await UpdateEdge();
    }

    private async UniTask UpdateEdge()
    {
        var startPos = node.transform.localPosition;
        var endPos = new Vector3(0, 0, 0);
        var direction = endPos - startPos;
        
        transform.localPosition = (startPos + endPos) * 0.5f;
        transform.localScale = new Vector3(
            radius.Value * 2f,
            (endPos - startPos).magnitude * heightScale.Value,
            radius.Value * 2f
        );

        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        await branchExpander.Expand(2f);
    }
}
