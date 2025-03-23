using UnityEngine;
using System;
using UnityEngine.Events;
using TMPro;
using Cysharp.Threading.Tasks;
[RequireComponent(typeof(Collider))]
public class NodeView : MonoBehaviour
{
    [SerializeField] private HistoryItem history;
    [SerializeField] private HistoryItem parentHistory;
    [SerializeField] private NodeView parentNode;
    [SerializeField] private Collider colider;
    [SerializeField] private float verticalOffset = 1f;
    [SerializeField] private float randomRange = 0.02f;
    [SerializeField] private int maxAttempts = 10;
    [SerializeField] private GameObject flower;
    private TextMeshPro textMeshPro;

    public UnityEvent onSelect = new UnityEvent();

    public async UniTask Start()
    {
        onSelect.AddListener(() =>
        {
            if (textMeshPro != null)
            {
                textMeshPro.text = $"Date: {history.date}\n" +
                     $"Title: {history.title}\n" +
                     $"Author: {history.author}\n";
            }
        });

        // flowerをランダムに表示
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            flower.SetActive(false);
            await UniTask.Delay(2600);
            flower.SetActive(true);
        }
        else
        {
            flower.SetActive(false);
        }
    }

    public async UniTask Set(HistoryItem _history, HistoryItem _parentHistory, NodeView _parentNode, TextMeshPro _textMeshPro, Transform parentTransform)
    {
        history = _history;
        parentHistory = _parentHistory;
        parentNode = _parentNode;
        textMeshPro = _textMeshPro;

        int attempts = 0;
        bool positionFound = false;

        // オブジェクトの名前を履歴のshaにする
        name = history.sha;

        if (parentNode == null)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            flower.SetActive(false);
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

            // randomOffsetをparentTransformのスケールに合わせる
            Debug.Log("parentTransform.localScale: " + parentTransform.localScale);
            randomOffset.Scale(parentTransform.localScale);
            // Vector3 newPosition = parentNode.transform.localPosition + randomOffset;
            Vector3 newPosition = randomOffset;

            // コライダーの衝突をチェック
            Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(newPosition), colider.bounds.size / 2);
            bool hasCollision = false;

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject && col.gameObject.GetComponent<NodeView>() != null)
                {
                    hasCollision = true;
                    Debug.Log("hasCollision: " + history.sha + " " + col.gameObject.name);
                    break;
                }
            }

            if (!hasCollision)
            {
                transform.localPosition = newPosition;
                positionFound = true;
            }

            attempts++;
        }

        Debug.Log($"NodeView.Set() attempts: {attempts}, {history.sha}, {transform.localPosition}");

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