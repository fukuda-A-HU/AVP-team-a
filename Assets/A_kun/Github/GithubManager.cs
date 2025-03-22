using UnityEngine;
using VContainer;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class GithubManager
{
    public readonly GitHubApiClient apiClient;

    [Inject]
    public GithubManager(GitHubApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public void SampleMethod()
    {
        Debug.Log("SampleMethod");
    }

    public async UniTask Test(string owner, string repo)
    {
        try
        {
            // 全てのブランチの履歴を取得
            var allHistory = await apiClient.GetAllBranchesHistoryAsync(
                owner,
                repo
            );
            Debug.Log($"Total history items: {allHistory.Count}");
            foreach (var item in allHistory)
            {
                string typeStr = item.type == HistoryType.COMMIT ? "Commit" : "Merge";
                Debug.Log($"{typeStr} on branch {item.branch}: {item.title} by {item.author} at {item.date}");
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"GitHub API Error: {e.Message}");
        }
    }

    public async UniTask<List<HistoryItem>> GetAllBranchesHistory(string owner, string repo)
    {
        return await apiClient.GetAllBranchesHistoryAsync(owner, repo);
    }
}
