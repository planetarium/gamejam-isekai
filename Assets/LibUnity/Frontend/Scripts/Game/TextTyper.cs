using System;
using System.Collections;
using TMPro;
using UnityEngine;

public static class TextTyper
{
    public static IEnumerator Play(TMP_Text typingText, string message, float speed, Action callback = null)
    {
        for (var i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        callback?.Invoke();
    }

    public static IEnumerator Rewind(TMP_Text typingText, string message, float speed, Action callback = null)
    {
        for (var i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, message.Length - i);
            yield return new WaitForSeconds(speed);
        }
        callback?.Invoke();
    }
}
