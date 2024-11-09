using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField]
    private int playerPropertyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerProperty playerProperty = GameManager.Instance.GameConfig.playerProperties[playerPropertyIndex];
        PlayerMovementController movement = GetComponent<PlayerMovementController>();
        movement.SetPlayerMovementProperty(playerProperty.maxSpeed, playerProperty.acelleration, playerProperty.friction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
