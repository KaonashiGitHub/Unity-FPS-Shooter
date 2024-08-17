using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    public AudioSource ShootingChannel;
    

    public AudioClip M1911Shot;
    public AudioClip M4_8Shot;

    public AudioSource reloadingSoundM4_8;
    public AudioSource reloadingSoundM1911;

    public AudioSource emptyMagazineSoundM1911;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon){
        switch(weapon){
            case WeaponModel.M1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.M4_8:
                ShootingChannel.PlayOneShot(M4_8Shot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon){
        switch(weapon){
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.M4_8:
                reloadingSoundM4_8.Play();
                break;
        }
    }
}
