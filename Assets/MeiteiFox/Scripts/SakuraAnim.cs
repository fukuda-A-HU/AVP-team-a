using UnityEngine;
using System.Collections;

public class SakuraAnim : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(TriggerRoutine());
    }

    IEnumerator TriggerRoutine()
    {
        yield return null; // 確実にループを維持するための追加
        while (true)
        {
            float waitTime = Random.Range(20f, 80f);
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("RotateTrigger");
        }
    }
}