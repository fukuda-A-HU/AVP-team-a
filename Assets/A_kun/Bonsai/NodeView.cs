using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class NodeView : MonoBehaviour
{
    [SerializeField] private HistoryItem history;
    [SerializeField] private HistoryItem parentHistory;
    [SerializeField] private NodeView parentNode;
    [SerializeField] private float verticalOffset = 0.5f;
    [SerializeField] private float randomRange = 0.5f;
    [SerializeField] private int maxAttempts = 10;
    private Transform bonsaiRoot;

    public void Set(HistoryItem _history, HistoryItem _parentHistory, NodeView _parentNode, Transform _bonsaiRoot)
    {
        history = _history;
        parentHistory = _parentHistory;
        parentNode = _parentNode;
        bonsaiRoot = _bonsaiRoot;
        int attempts = 0;
        bool positionFound = false;

        // nullである変数を確認
        Debug.Log($"history {history} parentHistory {parentHistory} parentNode {parentNode} bonsaiRoot {bonsaiRoot}");



        // オブジェクトの名前を履歴のshaにする
        name = history.sha;

        if (parentNode == null)
        {
            Debug.LogWarning("parentNode is null");
            transform.SetParent(bonsaiRoot);
            return;
        }

        // 親のNodeの子にオブジェクトを配置
        transform.SetParent(parentNode.transform);
        while (!positionFound && attempts < maxAttempts)
        {
            // ランダムなオフセットを生成
            Vector3 randomOffset = new Vector3(
                UnityEngine.Random.Range(-randomRange, randomRange),
                verticalOffset,
                UnityEngine.Random.Range(-randomRange, randomRange)
            );
            Vector3 newPosition = parentNode.transform.position + randomOffset;

            // コライダーの衝突をチェック
            Collider[] colliders = Physics.OverlapBox(newPosition, GetComponent<Collider>().bounds.size / 2);
            bool hasCollision = false;

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject && col.gameObject.GetComponent<NodeView>() != null)
                {
                    hasCollision = true;
                    break;
                }
            }

            if (!hasCollision)
            {
                transform.position = newPosition;
                positionFound = true;
            }

            attempts++;
        }

        if (!positionFound)
        {
            Debug.LogWarning($"Failed to find non-colliding position after {maxAttempts} attempts");
        }
    }

    public HistoryItem ReadData()
    {
        return history;
    }
}