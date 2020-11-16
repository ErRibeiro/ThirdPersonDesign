﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    [SerializeField]
    private float fadeMessageDuration = 1;
    private CanvasGroup canvasGroup;
    private TMP_Text messageText;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponent<TMP_Text>();

        canvasGroup.alpha = 0;

    }

    private IEnumerator ShowMessage(string message, float duration) {
        canvasGroup.alpha = 1;
        messageText.text = message;
        
        yield return new WaitForSeconds(duration);
        float fadeElapsedTime = 0;
        float fadeStartTime = Time.time; 
        while (fadeElapsedTime < fadeMessageDuration)
        {
            fadeElapsedTime = Time.time - fadeStartTime;
            canvasGroup.alpha = 1 - fadeElapsedTime / fadeMessageDuration;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
   
    private void OnContextualMessageTriggered(string message, float messageDuration)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message, messageDuration));

    }
    private void OnEnable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered += OnContextualMessageTriggered;
    }
    private void OnDisable()
    {
        ContextualMessageTrigger.ContextualMessageTriggered -= OnContextualMessageTriggered;
    }

 }