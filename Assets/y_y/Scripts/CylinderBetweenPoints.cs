using UnityEngine;

public class CylinderBetweenPoints : MonoBehaviour
{
    public GameObject cylinderPrefab; // InspectorでCylinderプリファブをアサイン
    public Vector3 pointA; // 始点
    public Vector3 pointB; // 終点
    public float cylinderWidth = 1.0f; // シリンダーの太さ

    private GameObject currentCylinder; // 現在表示中のシリンダー
    private Vector3 lastPointA; // 前回の始点
    private Vector3 lastPointB; // 前回の終点

    void Update()
    {
        // 始点または終点が変更された場合にのみ更新
        if (pointA != lastPointA || pointB != lastPointB)
        {
            UpdateCylinder();
            lastPointA = pointA;
            lastPointB = pointB;
        }
    }

    void UpdateCylinder()
    {
        // 既存のシリンダーがあれば削除
        if (currentCylinder != null)
        {
            Destroy(currentCylinder);
        }

        // 2点間の距離を計算
        float distance = Vector3.Distance(pointA, pointB);
        
        // 2点間の中間点を計算
        Vector3 centerPosition = (pointA + pointB) / 2f;
        
        // 新しいシリンダーを生成
        currentCylinder = Instantiate(cylinderPrefab, centerPosition, Quaternion.identity);
        
        // 2点間の方向を計算
        Vector3 direction = (pointB - pointA).normalized;
        
        // 回転を設定
        currentCylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        
        // スケールを調整
        Vector3 newScale = new Vector3(cylinderWidth, distance / 2f, cylinderWidth);
        currentCylinder.transform.localScale = newScale;
    }

    void OnDestroy()
    {
        if (currentCylinder != null)
        {
            Destroy(currentCylinder);
        }
    }
}