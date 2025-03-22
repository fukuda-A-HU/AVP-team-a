using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

public class GitHubApiClient
{
    private const string BASE_URL = "https://github-api-a-kun-e5531c2ca510.herokuapp.com"; // FastAPIサーバーのURL
    private static GitHubApiClient instance;
    private readonly HttpClient httpClient;

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
            throw new Exception($"リクエストエラー: {e.Message}", e);
        }
    }
}

