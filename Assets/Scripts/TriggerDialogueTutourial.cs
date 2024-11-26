using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueOnStartTutouirla : MonoBehaviour
{
    private DialogueManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<DialogueManager>();
        manager.TriggerDialogue();
    }
}
