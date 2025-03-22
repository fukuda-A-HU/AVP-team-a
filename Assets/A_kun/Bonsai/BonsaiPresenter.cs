using UnityEngine;
using VContainer.Unity;

public class BonsaiPresenter : IStartable
{
    public readonly BonsaiManager manager;
    public readonly BonsaiView view;

    public BonsaiPresenter(BonsaiManager manager, BonsaiView view)
    {
        this.manager = manager;
        this.view = view;
    }

    public void Start()
    {
        // 
    }
}