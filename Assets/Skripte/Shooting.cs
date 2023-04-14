using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    int totalAmmo, clipBullets, restAmmo, restAmmoMax = 120, totalAmmoMAX = 150, clipBulletsMAX = 30;
    float fireRate = 0f, cooldown = 0f;


    GameObject bullet;

    public GameObject bulletPrefab;
    public GameObject bulletStartPosition;

    public TextMeshProUGUI magazineText;

    public AudioSource shootingAudio;
    public AudioSource reloadingAudio;
    public AudioSource rafalAudio;

    Animator animator;

    public static Vector3 bulletDir;

    void Start()
    {
        animator = GetComponent<Animator>();
        totalAmmo = 150;
        clipBullets = 30;
        restAmmo = totalAmmo - clipBullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.gameOver)
        {
            shootingAudio.Stop();
            clipBullets = 30;
            totalAmmo = 150;
            restAmmo = 120;
            ammoUI();
        }
        if (GameManager.gameState == GameManager.GameState.running)
        {
            //Rafalno pucanje
            CheckShooting();
            //Pucanje na klik
            cooldown -= Time.deltaTime;
            ShootOnClick();

            CheckReload();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetBool("isShooting", false);
            }
        }
    }

    private void CheckShooting()
    {
        if (Input.GetMouseButton(0))
        {

            if (!animator.GetBool("isReloading"))
            {
                StartShooting();
                animator.SetBool("isShooting", true);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            rafalAudio.Stop();
            fireRate = 0f;
        }
    }
    private void StartShooting()
    {
        /*
         * Napravi dugme koje ce biti ispaljeno
         */
        
        fireRate += Time.deltaTime;
        if (clipBullets > 0 && fireRate >0.25)
        {
            rafalAudio.Play();
            animator.Play("Shoot");
            fireRate = 0f;
            bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
            bullet.AddComponent<BoxCollider>();

            bullet.transform.position = bulletStartPosition.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.Rotate(0, 180, 0);

            clipBullets--;
            ammoUI();
            totalAmmo = restAmmo + clipBullets;
        }
        else if(clipBullets==0)
        {
            rafalAudio.Stop();
        }
        
        
    }
    private void ShootOnClick()
    {
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {

            cooldown = 0.25f;
            if (!animator.GetBool("isReloading"))
            {
                StartShootOnClick();
                animator.SetBool("isShooting", true);
            }

        }
    }
    private void StartShootOnClick()
    {

        if(clipBullets>0)
        {
            shootingAudio.Play();
            animator.Play("Shoot");
            
            bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
            bullet.AddComponent<BoxCollider>();


            bullet.transform.position = bulletStartPosition.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.Rotate(0, 180, 0);

            clipBullets--;
            ammoUI();
            totalAmmo = restAmmo + clipBullets;
            //animator.SetBool("isShooting", false);
            //Debug.Log("Ja sam pozvan");
        }
    }
    private void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (restAmmo > 0 && clipBullets < 30)
            {
                animator.SetBool("isReloading", true);
                Reload();
                Invoke("SetIsRealodingToFalse",2.708f);
            }
        }
    }

    private void SetIsRealodingToFalse()
    {
        animator.SetBool("isReloading", false);
    }

    private void Reload()
    {
        reloadingAudio.Play();
        animator.Play("reload");
        if (clipBullets+restAmmo>=30)
        {
            
            restAmmo = restAmmo - (clipBulletsMAX - clipBullets);
            clipBullets = clipBulletsMAX;
        }
        else
        {
            clipBullets += restAmmo;
            restAmmo = 0; 
        }
        ammoUI();
    }
    public void AmmoPickup()
    {
        if(restAmmo<restAmmoMax)
        {
            if (restAmmoMax - restAmmo > 30)
            {
                restAmmo += 30;
                ammoUI();
            }
            else
            {
                restAmmo += restAmmoMax - restAmmo;
                ammoUI();
            }
            
        }
    }
    private void ammoUI()
    {
        magazineText.text = clipBullets.ToString() + "/" + restAmmo.ToString();
    }
    public float getAmmo()
    {
        return restAmmo;
    }
}
