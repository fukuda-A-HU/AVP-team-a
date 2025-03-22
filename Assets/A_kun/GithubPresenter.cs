using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GithubPresenter : IStartable
{    public readonly GithubView view;
    public readonly GithubManager manager;

    [Inject]
    public GithubPresenter(GithubManager manager, GithubView view)
    {
        this.manager = manager;
        this.view = view;
    }

    public void Start()
    {
        view.button.onClick.AddListener(() =>
        {
            manager.SampleMethod();
            manager.Test("fukuda-A-HU", "AVP-team-a");
        });
    }
}
