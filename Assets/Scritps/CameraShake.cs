using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject ShakeFX;
    public float ShakeDuration;

    private void Start()
    {
        ShakeFX.SetActive(false);
    }

    public void StartShake()
    {
        StopAllCoroutines();
        StartCoroutine(Shake(ShakeDuration));
    }

    private IEnumerator Shake(float t)
    {
        ShakeFX.SetActive(true);
        yield return new WaitForSeconds(t);
        ShakeFX.SetActive(false);
    }
}