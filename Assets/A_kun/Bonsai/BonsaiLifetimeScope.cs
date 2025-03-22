using VContainer;
using VContainer.Unity;

public class BonsaiLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<BonsaiManager>(Lifetime.Scoped);
        builder.Register<BonsaiView>(Lifetime.Scoped);
        
        builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
        {
            entryPoints.Add<BonsaiPresenter>();
        });
    }
}
