using System.Collections;
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

    // Update is called once per frame
    void Update()
    {

    }
    private void ShowMessage(string message, float duration) {
        messageText.text = message;
    }
   
    
       
    
}
