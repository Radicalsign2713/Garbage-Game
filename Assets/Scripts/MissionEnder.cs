using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionEnder : MonoBehaviour
{
    private PlayerMovementController player;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Steve-E").GetComponent<PlayerMovementController>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.battery <= 0){
            player.game_frozen = true;
            renderer.color = Color.white;
            if (Input.GetKeyDown("space")){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
