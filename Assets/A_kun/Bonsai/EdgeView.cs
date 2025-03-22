using R3;
using UnityEngine;

public class EdgeView : MonoBehaviour
{
    public SerializableReactiveProperty<float> radius = new SerializableReactiveProperty<float>(0.1f); // 円柱の半径
    public SerializableReactiveProperty<float> heightScale = new SerializableReactiveProperty<float>(1f); // 高さのスケール

    [SerializeField] private NodeView startNode;
    [SerializeField] private NodeView endNode;

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

    public void Set(NodeView start, NodeView end)
    {
        startNode = start;
        endNode = end;

        if (startNode == null || endNode == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(startNode.transform);

        UpdateEdge();
    }

    private void UpdateEdge()
    {
        Vector3 startPos = startNode.transform.position;
        Vector3 endPos = endNode.transform.position;
        Vector3 direction = endPos - startPos;
        float length = direction.magnitude;

        if (length < 0.001f) return;

        // 方向ベクトルを正規化
        direction.Normalize();

        // 円柱の向きを計算
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.rotation = rotation;

        // 円柱の中心位置を計算
        transform.position = (startPos + endPos) * 0.5f;

        // スケールを設定
        transform.localScale = new Vector3(radius.Value * 2f, length * heightScale.Value, radius.Value * 2f);
    }
}
