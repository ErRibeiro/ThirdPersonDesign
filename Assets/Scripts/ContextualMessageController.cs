﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextualMessageController : MonoBehaviour
{
    // Start is called before the first frame update
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
        canvasGroup.alpha = 0;
    }
   
    private void OnContextualMessageTriggered(string message, float messageDuration)
    {
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
