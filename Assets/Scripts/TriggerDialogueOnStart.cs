using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
    }
}
