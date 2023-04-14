using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    float timeToDie = 4f, damage;
    [SerializeField] float speed = 7f;

    MonkeyBehaviour monkeyBehaviour;

    Vector3 bulletDir;

    Ray ray;

    private void Awake()
    {
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        
        // Podesavanje da metak putuje gde se nacilja.
        Vector3 dirPoint;
        


        dirPoint = ray.GetPoint(10);
        bulletDir = dirPoint - transform.position;
        damage = 30;
    }

    void Update()
    {
        
        transform.position += bulletDir * Time.deltaTime * speed;
        SelfDesctruction();  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Enemy")
        {
            monkeyBehaviour = collision.transform.GetComponent<MonkeyBehaviour>();
            monkeyBehaviour.loseHealth(damage);
        }
        Destroy(gameObject);
    }
    private void SelfDesctruction()
    {
        timeToDie -= Time.deltaTime;
        if(timeToDie<=0)
        {
            Destroy(gameObject);
            timeToDie = 4f;
        }
    }

}
