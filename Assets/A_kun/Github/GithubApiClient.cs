using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class GitHubApiClient
{
    private string BASE_URL = "https://github-api-a-kun-e5531c2ca510.herokuapp.com"; // FastAPIサーバーのURL
    private string ALTERNATE_BASE_URL = "http://localhost:8000"; // FastAPIサーバーのURL
    private static GitHubApiClient instance;
    private readonly HttpClient httpClient;
    private const int MAX_RETRIES = 3;
    private const float RETRY_DELAY = 1f; // 秒

    private bool isUsingAlternateBaseUrl = false;

    public static GitHubApiClient Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GitHubApiClient();
            }
            return instance;
            
        }
    }

    private GitHubApiClient()
    {
        httpClient = new HttpClient();

        if (isUsingAlternateBaseUrl)
        {
            BASE_URL = ALTERNATE_BASE_URL;
        }
    }

    public async UniTask<List<CommitInfo>> GetCommitsAsync(string owner, string repo, string branch = "main")
    {
        string url = $"{BASE_URL}/commits/{owner}/{repo}?branch={branch}";
        return await MakeRequestAsync<List<CommitInfo>>(url);
    }

    public async UniTask<List<MergeInfo>> GetMergesAsync(string owner, string repo)
    {
        string url = $"{BASE_URL}/merges/{owner}/{repo}";
        return await MakeRequestAsync<List<MergeInfo>>(url);
    }

    public async UniTask<List<HistoryItem>> GetHistoryAsync(string owner, string repo, string branch = "main")
    {
        string url = $"{BASE_URL}/history/{owner}/{repo}?branch={branch}";
        return await MakeRequestAsync<List<HistoryItem>>(url);
    }

    public async UniTask<List<HistoryItem>> GetAllBranchesHistoryAsync(string owner, string repo)
    {
        string url = $"{BASE_URL}/all-branches-history/{owner}/{repo}";
        return await MakeRequestAsync<List<HistoryItem>>(url);
    }

    private async UniTask<T> MakeRequestAsync<T>(string url)
    {
        int retryCount = 0;
        while (retryCount < MAX_RETRIES)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception e)
                {
                    throw new Exception($"JSON解析エラー: {e.Message}", e);
                }
            }
            catch (Exception e)
            {
                retryCount++;
                if (retryCount >= MAX_RETRIES)
                {
                    throw new Exception($"リクエストエラー (試行回数: {retryCount}): {e.Message}", e);
                }
                Debug.LogWarning($"リクエスト失敗 (試行回数: {retryCount}/{MAX_RETRIES})。{RETRY_DELAY}秒後に再試行します。");
                await UniTask.Delay(TimeSpan.FromSeconds(RETRY_DELAY));
            }
        }
        throw new Exception("予期せぬエラーが発生しました。");
    }
}

