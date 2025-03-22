using UnityEngine;

public class BonsaiGenaration_test_yy : MonoBehaviour
{
    public GameObject cylinderPrefab; // InspectorでCylinderプリファブをアサイン

    void Start()
    {
        // 木の構造を定義（各距離が0.5〜1程度になるように設定）
        Vector3 root = new Vector3(0, 0, 0);          // 根
        Vector3 trunkEnd = new Vector3(0, 0.7f, 0);   // 幹の先端 (距離: 0.7)
        Vector3 branch1End = new Vector3(0.5f, 1.2f, 0);  // 枝1の先端 (距離: ~0.71)
        Vector3 branch2End = new Vector3(-0.5f, 1.2f, 0); // 枝2の先端 (距離: ~0.71)
        Vector3 branch3End = new Vector3(0, 1.2f, 0.5f);  // 枝3の先端 (距離: ~0.71)
        Vector3 branch4End = new Vector3(0, 1.2f, -0.5f); // 枝4の先端 (距離: ~0.71)

        // 幹を生成
        CreateCylinderBetweenPoints(root, trunkEnd);

        // 枝を生成
        CreateCylinderBetweenPoints(trunkEnd, branch1End);
        CreateCylinderBetweenPoints(trunkEnd, branch2End);
        CreateCylinderBetweenPoints(trunkEnd, branch3End);
        CreateCylinderBetweenPoints(trunkEnd, branch4End);
    }

    void CreateCylinderBetweenPoints(Vector3 startPoint, Vector3 endPoint)
    {
        // 二点間の距離を計算
        float distance = Vector3.Distance(startPoint, endPoint);

        // 二点間の中間点を計算
        Vector3 centerPosition = (startPoint + endPoint) / 2f;

        // シリンダーを生成
        GameObject cylinder = Instantiate(cylinderPrefab, centerPosition, Quaternion.identity);

        // 二点間の方向を計算
        Vector3 direction = (endPoint - startPoint).normalized;

        // 回転を設定（シリンダーのY軸を方向に合わせる）
        cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        // スケールを調整（太さ1、高さを距離に基づいて設定）
        Vector3 newScale = new Vector3(1.0f, distance / 2f, 1.0f);
        cylinder.transform.localScale = newScale;
    }
}
