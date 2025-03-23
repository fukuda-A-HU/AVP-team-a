using UnityEngine;
using System.Collections;

public class SakuraAnim2: MonoBehaviour
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
            float waitTime = Random.Range(10f, 40f);
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(RotateZAxis(360f, 1f)); // 1秒で360度回転
        }
    }

    IEnumerator RotateZAxis(float angle, float duration)
    {
        float elapsedTime = 0f;
        float startZ = transform.eulerAngles.z;
        float targetZ = startZ + angle;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t); // 加速して減速するイージング
            float newZ = Mathf.Lerp(startZ, targetZ, smoothT);
            transform.rotation = Quaternion.Euler(0, 0, newZ);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, targetZ);
    }
}

