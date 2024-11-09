using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameSate {
    playMode,
    waitForCharge,
    charging,
}

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    private int playerPropertyIndex = 0;

    private GameSate state = GameSate.playMode;
    public GameSate State { get { return state; } }

    [SerializeField]
    private Button chargePromptBtn;
    [SerializeField]
    private Button beginChargingBtn;

    // Start is called before the first frame update
    void Start()
    {
        PlayerProperty playerProperty = GameManager.Instance.GameConfig.playerProperties[playerPropertyIndex];
        PlayerMovementController movement = GetComponent<PlayerMovementController>();
        movement.SetPlayerMovementProperty(playerProperty.maxSpeed, playerProperty.acelleration, playerProperty.friction);

        progressBar.gameObject.SetActive(false);
    }

    

    public void BeginCharging()
    {
        state = GameSate.charging;

        progressBar.gameObject.SetActive(true);
        beginChargingBtn.gameObject.SetActive(false);
    }
    public void WaitForCharge()
    {
        state = GameSate.waitForCharge;
        progressBar.gameObject.SetActive(false);
    }
    public void EnterPlayMode()
    {
        state = GameSate.playMode;
        progressBar.gameObject.SetActive(false);
    }

    //public Action 

    private float timerCharging = 0;
    [SerializeField, Header("charging time")]
    private float chargingDuration = 30;
    [SerializeField]
    private Timer timerScript;
    [SerializeField]
    private Image progressBar;

    private void Update()
    {
        if(state == GameSate.charging)
        {
            timerCharging += Time.deltaTime;
            progressBar.fillAmount = timerCharging / chargingDuration;

            if (timerCharging > chargingDuration)
            {
                EnterPlayMode();
                timerScript.ResetTimer();
                timerCharging = 0;
            }
        }
    }
}
