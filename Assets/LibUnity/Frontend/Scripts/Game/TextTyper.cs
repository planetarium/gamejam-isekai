using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class TextTyper
{
    public static bool IsRendered;
    public static bool IsSuccess;

    public static IEnumerator Play(Text typingText, string message, float speed, Action<bool> callback = null)
    {
        for (var i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        callback?.Invoke(true);
    }

    public static IEnumerator PlayWithResource(Text typingText,
        string message,
        float speed,
        Slider progressBar,
        Text progressText,
        Action<bool> callback = null)
    {
        IsRendered = false;
        for (var i = 0; i < message.Length; i++)
        {
            if (IsRendered)
            {
                progressBar.value = 1;
                progressText.text = $"스토리를 발굴하는 중...100%";
                typingText.text = message;
                callback?.Invoke(IsSuccess);
                yield break;
            }

            var ratio = i / (float) message.Length;
            progressBar.value = ratio;
            progressText.text = $"스토리를 발굴하는 중...{(int) ((ratio * 100f))}%";
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }

        while (!IsRendered)
        {
            yield return null;
        }
        
        progressBar.value = 1;
        progressText.text = $"스토리를 발굴하는 중...100%";
        typingText.text = message;
        callback?.Invoke(true);
    }

    public static IEnumerator Rewind(Text typingText, 
                                     string preMessage, 
                                     string message,
                                     float speed,
                                     Slider progressBar,
                                     Text progressText,
                                     Action<bool> callback = null)
    {
        for (var i = 0; i <= message.Length; i++)
        {
            var length = message.Length - i;
            var ratio = length / (float) preMessage.Length;
            progressBar.value = ratio;
            progressText.text = $"스토리가 사라지고 있습니다...{(int) (ratio * 100f)}%";
            typingText.text = message.Substring(0, length);
            yield return new WaitForSeconds(speed);
        }

        callback?.Invoke(true);
    }
}