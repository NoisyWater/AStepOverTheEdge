using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public Gun gun;
    private bool isHoldingShoot = false;

    public RandomSoundPlayer soundPlayer;

    void OnShoot()
    {
        isHoldingShoot = true;
        if (soundPlayer != null)
        {
            soundPlayer.PlayRandomSound();
        }
    }

    void OnShootRelease()
    {
        isHoldingShoot = false;
    }

    void OnReload()
    {
        if(gun != null)
        {
            gun.TryReload();
        }
    }

    void Update()
    {
        if(isHoldingShoot && gun != null)
        {
            gun.Shoot();
        }
    }
}
