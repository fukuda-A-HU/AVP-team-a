using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GithubPresenter : IStartable
{
    public readonly GithubManager manager;
    public readonly GithubView view;

    [Inject]
    public GithubPresenter(GithubManager manager, GithubView view)
    {
        this.manager = manager;
        this.view = view;
    }

    public void Start()
    {
        view.button.onClick.AddListener(async () =>
        {
            manager.SampleMethod();
            await manager.Test("fukuda-A-HU", "AVP-team-a");
            var history = await manager.GetAllBranchesHistory("fukuda-A-HU", "AVP-team-a");
        });
    }
}
