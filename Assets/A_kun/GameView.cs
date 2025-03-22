using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class GameView : MonoBehaviour
{
    [SerializeField] private Button ReloadButton;
    public TextMeshPro descriptionText;
    public UnityEvent onReload = new UnityEvent();
    public GameObject edgePrefab;
    public GameObject nodePrefab;


    public void Start()
    {
        if (ReloadButton != null)
        {
            ReloadButton.onClick.AddListener(() =>
            {
                onReload.Invoke();
            });
        }
        else
        {
            onReload.Invoke();
        }
    }
}
