using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TreeGrowManager : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab; // 木のプレハブ
    [SerializeField] private TMP_InputField nameInputField; // 名前入力用のInputField
    [SerializeField] private Button growButton; // 成長/生成ボタン

    private Dictionary<string, GameObject> trees = new Dictionary<string, GameObject>(); // 名前と木の対応を管理
    private const float CIRCLE_RADIUS = 0.1f; // 円の半径

    private void Start()
    {
        // ボタンにリスナーを追加
        growButton.onClick.AddListener(OnGrowButtonClicked);
    }

    private void OnGrowButtonClicked()
    {
        string treeName = nameInputField.text.Trim();

        // 名前が空でないかチェック
        if (string.IsNullOrEmpty(treeName))
        {
            Debug.LogWarning("木の名前を入力してください");
            return;
        }

        // 既存の木があるかチェック
        if (trees.ContainsKey(treeName))
        {
            // 既存の木を1.2倍に成長させる
            GameObject existingTree = trees[treeName];
            existingTree.transform.localScale *= 1.2f;
            Debug.Log($"{treeName} が成長しました。現在のスケール: {existingTree.transform.localScale}");
        }
        else
        {
            // 木の生成位置を決定
            Vector3 spawnPosition = GetSpawnPosition(treeName);
            
            // 新しい木を生成
            GameObject newTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            newTree.name = treeName; // 木に名前を設定
            trees.Add(treeName, newTree); // 辞書に追加
            Debug.Log($"{treeName} が {spawnPosition} に新しく生成されました");
        }

        // InputFieldをクリア（任意）
        nameInputField.text = "";
    }

    private Vector3 GetSpawnPosition(string treeName)
    {
        if (treeName.ToLower() == "main")
        {
            // "main"の場合は原点(0,0,0)
            return Vector3.zero;
        }
        else
        {
            // 原点を中心とする半径1.5mの円内でランダムな位置
            Vector2 randomPoint = Random.insideUnitCircle * CIRCLE_RADIUS;
            return new Vector3(randomPoint.x, 0f, randomPoint.y);
        }
    }

    // 必要に応じて木を削除するメソッド（オプション）
    public void RemoveTree(string treeName)
    {
        if (trees.ContainsKey(treeName))
        {
            Destroy(trees[treeName]);
            trees.Remove(treeName);
            Debug.Log($"{treeName} が削除されました");
        }
    }
}