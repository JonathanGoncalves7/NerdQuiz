using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    [SerializeField] float Duration;
    [SerializeField] float Magnitude;
    [SerializeField] GameObject PanelShake;

    public void Shake()
    {
        StartCoroutine(this.CRShake());
    }

    public IEnumerator CRShake()
    {
        Vector3 originalPos = PanelShake.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < Duration)
        {
            PanelShake.transform.localPosition = Random.insideUnitCircle * Magnitude;

            elapsed += Time.deltaTime;

            yield return null;
        }

        PanelShake.transform.localPosition = originalPos;
    }
}
