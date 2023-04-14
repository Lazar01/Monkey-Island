using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject enemyRadarPrefab;
    public GameObject player;
    private GameObject trackedObject;
    public GameObject k,j;

    private Ray ray;
    private Vector3 dirToEnemy;
    private void Awake()
    {
        trackedObject = gameObject;
        CreateRadarObjects();

        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(GameManager.gameState == GameManager.GameState.running && k!=null)
        {
            if (Vector3.Distance(player.transform.position, k.transform.position) >= 34.5)
            {
                dirToEnemy = transform.position - player.transform.position;
                ray = new Ray(player.transform.position, dirToEnemy);
                j.SetActive(true);
                j.transform.position = ray.GetPoint(35);
                k.SetActive(false);
            }
            else
            {
                j.SetActive(false);
                k.SetActive(true);
            }
        }
    }
    public void CreateRadarObjects()
    {
        k = Instantiate(enemyRadarPrefab, trackedObject.transform.position, Quaternion.identity) as GameObject;
        k.transform.SetParent(trackedObject.transform);
        j = Instantiate(enemyRadarPrefab, trackedObject.transform.position, Quaternion.identity) as GameObject;
    }
}
