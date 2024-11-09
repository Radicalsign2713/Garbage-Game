using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChargeControl : MonoBehaviour
{
    [SerializeField]
    private Button chargePromptBtn;
    [SerializeField]
    private Button beginChargingBtn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Base") && PlayerControl.instance.State == GameSate.waitForCharge)
        {
            //state = GameSate.charging;
            beginChargingBtn.gameObject.SetActive(true);
            chargePromptBtn.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("ChargePrompt") && PlayerControl.instance.State == GameSate.waitForCharge)
        {
            //state = GameSate.charging;
            chargePromptBtn.gameObject.SetActive(true);
            beginChargingBtn.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Base") && PlayerControl.instance.State != GameSate.playMode)
        {
            //state = GameSate.waitForCharge;
            PlayerControl.instance.WaitForCharge();
            beginChargingBtn.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("ChargePrompt") && PlayerControl.instance.State == GameSate.waitForCharge)
        {
            chargePromptBtn.gameObject.SetActive(false);
        }
    }
}
