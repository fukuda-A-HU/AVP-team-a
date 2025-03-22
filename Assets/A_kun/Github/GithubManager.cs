using UnityEngine;
using VContainer;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.Animations;

public class GithubManager
{
    public readonly GitHubApiClient apiClient;

    [Inject]
    public GithubManager(GitHubApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public async UniTask Test(string owner, string repo)
    {
        try
        {
            // 全てのブランチの履歴を取得
            var allHistory = await GetAllBranchesHistory(owner, repo);

            foreach (var item in allHistory)
            {
                string typeStr = item.type == HistoryType.COMMIT ? "Commit" : "Merge";

                Debug.Log($"{typeStr} on branch {item.branch}: {item.title} by {item.author} at {item.date} ParentCommit: {item.parent_sha} ParentMessage: {item.parent_message} ParentAuthor: {item.parent_author} ParentDate: {item.parent_date} ParentUrl: {item.parent_url}");
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"GitHub API Error: {e.Message}");
        }
    }

    public async UniTask<List<HistoryItem>> GetAllBranchesHistory(string owner, string repo)
    {
        var allHistory = await apiClient.GetAllBranchesHistoryAsync(owner, repo);
        allHistory.Sort((a, b) => a.date.CompareTo(b.date));
        return allHistory;
    }
}
