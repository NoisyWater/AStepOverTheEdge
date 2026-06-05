using System.Collections; 
using UnityEngine;
public class Gun : MonoBehaviour
{
    public float reloadTime = 1f;
    public float fireRate = 1f;
    public int magSize = 20;

    public GameObject Spell;
    public Transform MagicSpawnPoint;

    public RandomSoundPlayer soundPlayer;

    public float recoilDistence = 1f;
    public float recoilSpeed = 15f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private Quaternion initalRotation;

    private Vector3 initalPosition;

    private Vector3 reloadRotationOffset = new Vector3(66, 58, 50);
    void Start()
    {
        currentAmmo = magSize;

        initalRotation = transform.localRotation;

        initalPosition = transform.localPosition;

        UIManager.Instance.ammoText.text = currentAmmo.ToString();
    }


    public void Shoot()
    {

        if (isReloading) return;

        if (Time.time < nextTimeToFire) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;
        UIManager.Instance.ammoText.text = currentAmmo.ToString();

        if (soundPlayer != null)
        {
            soundPlayer.PlayRandomSound();
        }

        Quaternion adjustedRotation = MagicSpawnPoint.rotation * Quaternion.Euler(0.5f, -4f, 0f);

        Instantiate(Spell, MagicSpawnPoint.position, adjustedRotation);

        StopCoroutine(nameof(Recoil));
        StartCoroutine(nameof(Recoil));
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Quaternion targetRotation = Quaternion.Euler(initalRotation.eulerAngles + reloadRotationOffset); 
        float halfReload = reloadTime / 2f;
        float t = 0f;

        while (t < halfReload)
        {
            t += Time.deltaTime;

            transform.localRotation = Quaternion.Slerp(initalRotation, targetRotation, t / halfReload);
            yield return null;
        }

        t = 0f;

        while (t < halfReload)
        {
            t += Time.deltaTime;

            transform.localRotation = Quaternion.Slerp(targetRotation, initalRotation, t / halfReload);
            yield return null;
        }

        currentAmmo = magSize;
        UIManager.Instance.ammoText.text = currentAmmo.ToString();
        isReloading = false;
    }

    public void TryReload()
    {
        if(isReloading) return;
        if (currentAmmo == magSize) return;
        StartCoroutine(Reload());
    }

    private IEnumerator Recoil()
    {
        Vector3 recoilTarget = initalPosition + new Vector3(recoilDistence, 0, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;

            transform.localPosition = Vector3.Lerp(initalPosition, recoilTarget, t);
            yield return null;
        }

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;

            transform.localPosition = Vector3.Lerp(recoilTarget, initalPosition, t);
            yield return null;
        }

        transform.localPosition = initalPosition;
    }
}