using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CreatureType { Common, Boss }

public class SpawnControler : MonoBehaviour
{
    public static SpawnControler Instance;

    [SerializeField]
    SpawnItem playerSpawn;
    [SerializeField]
    SpawnItem[] enemySpawns;

    public GameObject playerOnScene { get; private set; }
    public List<GameObject> enemiesOnScene { get; private set; }

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            Instance.enemiesOnScene = new List<GameObject>();
            Instance.StartupSpawn();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void StartupSpawn()
    {
        playerOnScene = Instantiate(playerSpawn.characterPrefab, playerSpawn.spawnPoints[0].position, Quaternion.identity);

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            var enemyType = enemySpawns[i];
            for (int n = 0; n < enemyType.spawnPoints.Length; n++)
            {
                var newEnemy = Instantiate(enemyType.characterPrefab, enemyType.spawnPoints[n].position, Quaternion.identity);
                enemiesOnScene.Add(newEnemy);
            }
        }
    }

    public GameObject GetClosestTarget(ShootAt shootAt)
    {
        GameObject target = null;

        switch (shootAt)
        {
            case ShootAt.enemies:
                target = GetClosestEnemyGO();
                break;
            case ShootAt.player:
                if(playerOnScene.activeSelf)
                    target = playerOnScene;
                break;
        }

        return target;
    }

    GameObject GetClosestEnemyGO()
    {
        GameObject closestEnemyGO = null;

        var playerPos = playerOnScene.transform.position;
        float? closestDistance = null;

        for (int i = 0; i < enemiesOnScene.Count; i++)
        {
            if (enemiesOnScene[i].activeSelf == false)
                continue;

            if(closestDistance == null)
            {
                closestDistance = Vector3.Distance(playerPos, enemiesOnScene[i].transform.position);
                closestEnemyGO = enemiesOnScene[i];
            }
            else
            {
                var currentItemDistance = Vector3.Distance(playerPos, enemiesOnScene[i].transform.position);
                if (currentItemDistance < closestDistance)
                {
                    closestDistance = currentItemDistance;
                    closestEnemyGO = enemiesOnScene[i];
                }
            }
        }

        return closestEnemyGO;
    }

    bool AllEnemiesDead()
    {
        for (int i = 0; i < enemiesOnScene.Count; i++)
        {
            if (enemiesOnScene[i].activeSelf)
                return false;
        }
        
        return true;
    }

    public void CreatureDie(GameObject creature, CreatureType type)
    {
        creature.SetActive(false);

        if (AllEnemiesDead())
        {
            switch (type)
            {
                case CreatureType.Common:
                    GameManager.Instance.LevelEnd(LevelResult.LevelWin);
                    break;

                case CreatureType.Boss:
                    GameManager.Instance.LevelEnd(LevelResult.BossWin);
                    break;
            }
        }
    }
}

[Serializable]
public class SpawnItem
{
    public GameObject characterPrefab;
    public Transform[] spawnPoints;
}