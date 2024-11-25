using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerCockroachDiolouge : MonoBehaviour
{
    private DialogueManager manager;
    private GameObject pointer;
    private PlayerMovementController player;

    // Start is called before the first frame update
    void Start()
    {
        manager =  GameObject.Find("OverlayDialogueManager").GetComponent<DialogueManager>();
        pointer = GameObject.Find("CockroachPointer");
        player = GameObject.Find("Steve-E").GetComponent<PlayerMovementController>();
    }
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.TriggerDialogue();
            Destroy(pointer);
            player.game_frozen = true;
        }
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
