using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnStart : MonoBehaviour
{
    private DialogueManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<DialogueManager>();
        manager.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isFinished)
        {
            // Troy Add scene switch here
        } 
    }
}
