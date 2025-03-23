using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BranchExpander : MonoBehaviour
{
    private Vector3 defaultScale;
    private Vector3 defaultPosition;

    public async UniTask Expand(float duration)
    {
        defaultScale = transform.localScale;
        Vector3 newScale = transform.localScale;
        newScale.y = 0;

        defaultPosition = transform.localPosition;
        // Vector3 newPosition = transform.localPosition;
        // newPosition.y = 0;
        Vector3 newPosition = new Vector3(0, 1, 0);

        Debug.Log($"defaultScale: {defaultScale}");
        Debug.Log($"defaultPosition: {defaultPosition}");

        // newScale, newPositionに設定する
        transform.DOScale(newScale, 0).SetEase(Ease.OutCubic);
        transform.DOLocalMove(newPosition, 0).SetEase(Ease.OutCubic);

        await UniTask.Delay((int)(1 * 1000));

        // defaultScale, defaultPositionに向かってアニメーションする
        transform.DOScale(defaultScale, duration).SetEase(Ease.OutCubic);
        transform.DOLocalMove(defaultPosition, duration).SetEase(Ease.OutCubic);

        await UniTask.Delay((int)(duration * 1000));
    }
}
