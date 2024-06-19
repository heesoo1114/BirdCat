using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartDelayText : MonoBehaviour
{
    private TextMeshProUGUI delayText;

    private void Awake()
    {
        delayText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(DelayLoop());
    }

    private IEnumerator DelayLoop()
    {
        yield return new WaitForSeconds(1f);
        delayText.text = "2";
        yield return new WaitForSeconds(1f);
        delayText.text = "1";
        yield return new WaitForSeconds(1f);
        delayText.text = "GO!";
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
