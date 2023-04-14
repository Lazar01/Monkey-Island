using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : MonoBehaviour
{
    public Shooting shootingScript;
    public GameManager gm;

    public float timeToDieBulletPickup=8f;
    public float timeToDieBananaPickup = 8f;

    float time=0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (transform.tag == "BulletsPickup")
            {
                if (shootingScript.getAmmo() < 120)
                {
                    shootingScript.AmmoPickup();
                    Destroy(gameObject);
                }
            }
            else if (transform.tag == "BananaPickup")
            {
                if (gm.GetLives() < 3)
                {
                    foreach (RawImage life in gm.GetImgsLives())
                    {
                        if (!life.IsActive())
                        {
                            life.gameObject.SetActive(true);
                            break;
                        }
                    }
                    gm.RestoreLife();
                }
                    if (gm.GetBananaPeels() < 3)
                        gm.BananaPeelAmmoRestore();                
                    Destroy(gameObject);
                
            }
        }
    }
    private void Start()
    {
        switch (tag)
        {
            case "BulletsPickup":
                time = timeToDieBulletPickup;
                break;
            case "BananaPickup":
                time = timeToDieBananaPickup;
                break;
        }
    }
    private void Update()
    {
        if (!(GameManager.gameState == GameManager.GameState.running) && !(GameManager.gameState == GameManager.GameState.pause))
        {
            Destroy(gameObject);
        }
        
        SelfDesctruction();
    }
    private void SelfDesctruction()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
