using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

  
    public AudioSource ShootingChannel;
   

    public AudioClip PistlM1911;
    public AudioClip M16Shot;

   
    public AudioSource reloadingSoundM16;
    public AudioSource reloadingSound1911;
    public AudioSource emptyMagazineSound1911;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
    }

   public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {

            case WeaponModel.Pistol1911:
                ShootingChannel.PlayOneShot(PistlM1911); 
                break;
            case WeaponModel.M16:
                //shootingSoundM16.Play(); // Playing different sounds in same channel
                ShootingChannel.PlayOneShot(M16Shot); //in this needs a original audio clip
                break;
        }
    }


    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {

            case WeaponModel.Pistol1911:
                reloadingSound1911.Play();
                break;
            case WeaponModel.M16:
                reloadingSoundM16.Play();
                break;
        }
    }
}
