using UnityEngine;
using VContainer;
using VContainer.Unity;
using ObservableCollections;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

public class GamePresenter : IStartable
{
    private readonly BonsaiManager bonsaiManager;
    private readonly GithubManager githubManager;
    private readonly GameView gameView;

    [Inject]
    public GamePresenter(
        BonsaiManager bonsaiManager,
        GithubManager githubManager,
        GameView gameView)
    {
        this.bonsaiManager = bonsaiManager;
        this.githubManager = githubManager;
        this.gameView = gameView;
    }

    public async void Start()
    {
        bonsaiManager.Initialize();

        gameView.onReload.AddListener(async () =>
        {
            Debug.Log("onReload");
            var histories = await githubManager.GetAllBranchesHistory("fukuda-A-HU", "ELF-SR2");
            Debug.Log($"histories: {histories.Count}");
            bonsaiManager.histories.Clear();
            foreach (var history in histories)
            {
                Debug.Log($"history: {history.title}");
                bonsaiManager.histories.Add(history);
            }   
        });
    }
}
