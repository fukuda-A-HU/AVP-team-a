using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GithubLifetimeScope : LifetimeScope
{
    [SerializeField] private GithubView view;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent<GithubView>(view);
        builder.Register<GithubManager>(Lifetime.Scoped);
        builder.Register<GitHubApiClient>(Lifetime.Scoped);

        // Register view
        builder.UseEntryPoints(Lifetime.Singleton, entryPoints => 
        {
            entryPoints.Add<GithubPresenter>();
        });
    }
}
