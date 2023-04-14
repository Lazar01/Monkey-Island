using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonkeyBehaviour : MonoBehaviour
{
    float hp, maxhp, randomX,randomZ;
    public float moveSpeed;
    int damage;

    Animator animator;
    //GameObject monkeySpawn;

    Vector3 dir;

    public GameObject tree;
    public GameObject monkeyPrefab;

    public GameManager gm;
    public Slider slider;

    public GameObject ammoPickupPrefab;
    public GameObject bananaPickupPrefab;
    public GameObject PickupParent;

    private Radar radar;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //monkeySpawn = new GameObject();
        SetHpAndMoveSpeed(gameObject.name);

        slider.value = CalculateHealth();

        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);

        randomX = Random.Range(10, 90);
        randomZ = Random.Range(20, 100);

        Ray ray = new Ray(tree.transform.position, new Vector3(randomX, 0, randomZ) - tree.transform.position);

        Vector3 spawnLoc = ray.GetPoint(40);
        spawnLoc.y = 0.5f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        transform.position = spawnLoc;
        transform.LookAt(tree.transform);
        dir = tree.transform.position - transform.position;

        radar = GetComponent<Radar>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();

        if (GameManager.gameState == GameManager.GameState.running && animator.GetBool("isRunning"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.LookAt(tree.transform);
            //rb.AddForce(dir * moveSpeed * Time.deltaTime, ForceMode.Force);
            //monkeySpawn.transform.LookAt(tree.transform);
        }
        else if(GameManager.gameState != GameManager.GameState.pause)
        {
            DestroyEnemyRadarClone();
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Tree")
        {
            animator.SetBool("isRunning", false);
            gm.LostLife(damage);
            DestroyEnemyRadarClone();
            Destroy(gameObject);
            
        }
        else if(collision.transform.tag=="WorldBounds")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
    public void loseHealth(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                switch (name)
                {
                    case "monkey(Clone)":
                        float random = Random.Range(1, 1);
                        if (random == 1)
                        {
                            GameObject ammoPickup = Instantiate(ammoPickupPrefab);
                            ammoPickup.transform.position = transform.position;
                            ammoPickup.transform.position = new Vector3(ammoPickup.transform.position.x, ammoPickup.transform.position.y + 0.5f, ammoPickup.transform.position.z);
                            ammoPickup.SetActive(true);
                            ammoPickup.transform.SetParent(PickupParent.transform);
                        }
                        DestroyEnemyRadarClone();
                        gm.KilledEnemy();
                        break;
                    case "gorilla(Clone)":
                        float random1 = Random.Range(1, 10);
                        if (random1 == 1)
                        {
                            GameObject ammoPickup = Instantiate(ammoPickupPrefab);
                            ammoPickup.transform.position = transform.position;
                            ammoPickup.transform.position = new Vector3(ammoPickup.transform.position.x, ammoPickup.transform.position.y + 0.5f, ammoPickup.transform.position.z);
                            ammoPickup.SetActive(true);
                            ammoPickup.transform.SetParent(PickupParent.transform);
                        }
                        else if(true)
                        {
                            GameObject bananaPickup = Instantiate(bananaPickupPrefab);
                            bananaPickup.transform.position = transform.position;
                            bananaPickup.transform.position = new Vector3(bananaPickup.transform.position.x, bananaPickup.transform.position.y + 0.5f, bananaPickup.transform.position.z);
                            bananaPickup.SetActive(true);
                            bananaPickup.transform.SetParent(PickupParent.transform);
                        }
                        DestroyEnemyRadarClone();
                        gm.KilledEnemy();
                        break;
                }

                

            }
        }
    }
    private void DestroyEnemyRadarClone()
    {
        if (radar.j != null)
            Destroy(radar.j);
        Destroy(gameObject);
    }
    float CalculateHealth()
    {
        return hp / maxhp;
    }
    private void SetHpAndMoveSpeed(string name)
    {
        switch (name)
        {
            case "monkey(Clone)":
                maxhp = 90;
                hp = maxhp;
                moveSpeed = 4f;
                damage = 1;
                break;
            case "gorilla(Clone)":
                maxhp = 180;
                hp = maxhp;
                moveSpeed = 3f;
                damage = 2;
                break;
            default:
                break;
        }
        
    }
}
