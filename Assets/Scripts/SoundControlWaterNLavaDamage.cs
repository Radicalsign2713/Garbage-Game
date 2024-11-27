using System.Collections;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    private PlayerMovementController player;
    public AudioSource waterSoundSource; // AudioSource for water sound (looping)
    public AudioSource damageSoundSource; // AudioSource for damage sound (one-shot)
    public AudioClip waterClip; // Clip for water sound
    public AudioClip damageClip; // Clip for damage sound

    private bool isPlayingWaterSound = false;

    void Start()
    {
        player = GetComponent<PlayerMovementController>();

        // Initialize the audio sources
        if (waterSoundSource != null && waterClip != null)
        {
            waterSoundSource.clip = waterClip;
            waterSoundSource.loop = true;
            waterSoundSource.playOnAwake = false;
        }

        if (damageSoundSource != null && damageClip != null)
        {
            damageSoundSource.clip = damageClip;
            damageSoundSource.loop = false;
            damageSoundSource.playOnAwake = false;
        }
    }

    void Update()
    {
        HandleWaterSound();
        HandleDamageSound();
    }

    void HandleWaterSound()
    {
        if (player.on_liquid && !player.on_island)
        {
            if (!isPlayingWaterSound && waterSoundSource != null)
            {
                waterSoundSource.Play();
                isPlayingWaterSound = true;
            }
        }
        else
        {
            if (isPlayingWaterSound && waterSoundSource != null)
            {
                waterSoundSource.Stop();
                isPlayingWaterSound = false;
            }
        }
    }

    void HandleDamageSound()
    {
        if (player.on_lava && damageSoundSource != null && !damageSoundSource.isPlaying)
        {
            damageSoundSource.Play();
            FlashRedEffect();
        }
    }

    void FlashRedEffect()
    {
        // Implement flashing red effect for the player taking damage
        // You could add logic here to make the player flash red
        // This could be done by altering the player's SpriteRenderer color, for example
    }
}
