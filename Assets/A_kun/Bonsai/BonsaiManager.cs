using System.Collections.Generic;
using UnityEngine;
using ObservableCollections;
using VContainer.Unity;
using VContainer;
using R3;

public class BonsaiManager : IStartable
{
    ObservableList<HistoryItem> histories = new ObservableList<HistoryItem>();
    ObservableList<NodeView> nodeViews = new ObservableList<NodeView>();
    ObservableList<EdgeView> edgeViews = new ObservableList<EdgeView>();
    
    public void ReloadBonsai(List<HistoryItem> histories)
    {
        histories.Clear();
        foreach (var history in histories)
        {
            histories.Add(history);
        }
    }

    public void AddHistory(HistoryItem history)
    {
        histories.Add(history);
    }

    public void Start()
    {
        histories.ObserveAdd().Subscribe(e =>
        {
            Debug.Log($"Add: {e.Value}");
        });

        histories.ObserveRemove().Subscribe(e =>
        {
            Debug.Log($"Remove: {e.Value}");
        });

        histories.ObserveReset().Subscribe(_ =>
        {
            Debug.Log("Reset");
        });

        histories.ObserveClear().Subscribe(_ =>
        {
            Debug.Log("Clear");
        });
    }

    
}
