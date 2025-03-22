using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameView gameView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GithubManager>(Lifetime.Scoped);
        builder.Register<BonsaiManager>(Lifetime.Scoped);
        builder.Register<GitHubApiClient>(Lifetime.Scoped);
        builder.RegisterComponent<GameView>(gameView);

        builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
        {
            entryPoints.Add<GamePresenter>();
        });
    }
}
