using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GithubLifetimeScope : LifetimeScope
{
    [SerializeField] private GithubView view;
    [SerializeField] private TreeGrowManager treeGrowManager;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent<GithubView>(view);
        builder.Register<GithubManager>(Lifetime.Scoped);
        builder.Register<GitHubApiClient>(Lifetime.Scoped);

        // 使いたいコンポーネントを取得して登録
        builder.Register<TreeGrowManager>(Lifetime.Scoped);

        // Register view
        builder.UseEntryPoints(Lifetime.Singleton, entryPoints => 
        {
            entryPoints.Add<GithubPresenter>();
        });
    }
}
