﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class GameThreadManager : MonoBehaviour
{
    public static GameThreadManager Instance
    {
        get { return _instance; }
    }

    private static GameThreadManager _instance;

    public UIHudManager uiHudManadger
    {
        get { return _uiHudManager; }
    }

    public int startEnemies = 2;
    public GameObject enemyPrefab;
    public GameObject pickupEnergyPrefab;
    public Transform[] spawnPoints;
    public GameObject playerBase;
    [HideInInspector]
    public List<Enemy> availableEnemies = new List<Enemy>();
    [HideInInspector]
    public List<EnergyPickup> availablePickEnergies = new List<EnergyPickup>();
    public VignetteAndChromaticAberration vignetteEffect;

    private UIHudManager _uiHudManager;

    void Awake()
    {
        _instance = this;
        _uiHudManager = FindObjectOfType<UIHudManager>();
        //
        StartCoroutine(StartSpawn(7f));
        //
        SpawnEnergyRandom();
        SpawnEnergyRandom();
        SpawnEnergyRandom();
        SpawnEnergyRandom();
        SpawnEnergyRandom();
        SpawnEnergyRandom();
        SpawnEnergyRandom();
    }

    private void SpawnEnemy(Vector3 pos)
    {
        var enemyGo = (GameObject)Instantiate(enemyPrefab, pos, Quaternion.identity);
        var enemyCom = enemyGo.GetComponent<Enemy>();
        enemyCom.target = playerBase.transform;
        availableEnemies.Add(enemyCom);
    }

    private void SpawnEnemyRandom()
    {
        var pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        var enemyGo = (GameObject)Instantiate(enemyPrefab, pos, Quaternion.identity);
        var enemyCom = enemyGo.GetComponent<Enemy>();
        enemyCom.target = playerBase.transform;
        availableEnemies.Add(enemyCom);
    }

    private IEnumerator StartSpawn(float spawnDelay)
    {
        for (var i = 0; i < startEnemies; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemyRandom();
        }
    }

    private void SpawnEnergyRandom()
    {
        var targetPos = new Vector3(Random.Range(-120f, 120f), 0f, Random.Range(-120f, 120f));
        var energyGo = (GameObject)Instantiate(pickupEnergyPrefab, targetPos, Quaternion.identity);
        var energyCom = energyGo.GetComponent<EnergyPickup>();
        availablePickEnergies.Add(energyCom);
    }
}
