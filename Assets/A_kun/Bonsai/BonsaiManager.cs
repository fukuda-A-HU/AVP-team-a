using System.Collections.Generic;
using UnityEngine;
using ObservableCollections;
using VContainer.Unity;
using VContainer;
using R3;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class BonsaiManager
{
    public readonly Transform bonsaiRoot;
    public readonly GameObject nodePrefab;
    // private GameObject edgePrefab;

    public ObservableList<HistoryItem> histories = new ObservableList<HistoryItem>();

    public ObservableList<NodeView> nodeViews = new ObservableList<NodeView>();

    // private ObservableList<EdgeView> edgeViews = new ObservableList<EdgeView>();
    private Vector3 rootPosition;

    public BonsaiManager(GameView gameView)
    {
        this.bonsaiRoot = gameView.transform;
        this.nodePrefab = gameView.nodePrefab;
        // this.edgePrefab = gameView.edgePrefab;
    }

    public void Initialize()
    {
        rootPosition = bonsaiRoot.position;

        histories.ObserveAdd().Subscribe(e =>
        {
            var parentHistory = histories.FirstOrDefault(x => x.sha == e.Value.parent_sha);
            NodeView parentNode = null;
            if (parentHistory != null)
            {
                parentNode = nodeViews.FirstOrDefault(x => x.ReadData().sha == parentHistory.sha);
            }

            Debug.Log($"新しい履歴が追加されました:\n" +
                     $"Type: {e.Value.type}\n" +
                     $"Date: {e.Value.date}\n" +
                     $"Title: {e.Value.title}\n" +
                     $"Author: {e.Value.author}\n" +
                     $"Branch: {e.Value.branch}\n" +
                     $"SHA: {e.Value.sha}\n" +
                     $"Parent SHA: {e.Value.parent_sha}");

            if (parentHistory != null)
            {
                Debug.Log($"親の履歴情報:\n" +
                         $"Type: {parentHistory.type}\n" +
                         $"Date: {parentHistory.date}\n" +
                         $"Title: {parentHistory.title}\n" +
                         $"Author: {parentHistory.author}\n" +
                         $"Branch: {parentHistory.branch}\n" +
                         $"SHA: {parentHistory.sha}");
            }
            else
            {
                Debug.Log("親の履歴が見つかりませんでした。");
            }

            var nodeView = UnityEngine.Object.Instantiate(nodePrefab, rootPosition, Quaternion.identity);
            var node = nodeView.GetComponent<NodeView>();

            node.Set(e.Value, parentHistory, parentNode, bonsaiRoot);

            nodeViews.Add(node);
            
        });

        histories.ObserveClear().Subscribe(_ =>
        {
            Debug.Log("Clear");
            foreach (var nodeView in nodeViews)
            {
                UnityEngine.Object.Destroy(nodeView.gameObject);
            }
            nodeViews.Clear();
        });
    }
}
