using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualMessageTrigger : MonoBehaviour
{
    [SerializeField]
     [TextArea(3,5)]
    private string message = "Default Message";
    [SerializeField]
    private float messageDuration = 5f;
    public static event Action<string, float> ContextualMessageTriggered;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ContextualMessageTriggered != null)
            {
                ContextualMessageTriggered.Invoke(message, messageDuration);
            }
            
        }
    }
}
