using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Entities;
using UnityEngine.UI;

public class WorldSerialization : MonoBehaviour
{
    public WorldData worldData;
    public string DataPath;

    [Header("Add Enemy")]
    public InputField addEnemy_name;
    public InputField addEnemy_hp;
    public InputField addEnemy_damage;
    public InputField addEnemy_movementSpeed;
    [Header("Add Player")]
    public InputField addPlayer_name;
    public InputField addPlayer_hp;
    public InputField addPlayer_damage;
    public InputField addPlayer_movementSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightControl))
        {
            print("Serializado!");
            worldData.Serialize(DataPath, false);
        }
    }

    public void AddEnemy()
    {
        var enemy = new Enemy();
        var enemyStats = new Stats(addEnemy_name.text,
                                   int.Parse(addEnemy_hp.text),
                                   int.Parse(addEnemy_damage.text),
                                   float.Parse(addEnemy_movementSpeed.text));
        enemy.stats = enemyStats;
        worldData.enemyList.Add(enemy);

        addEnemy_name.text = "EnemyName";
        addEnemy_hp.text = "000";
        addEnemy_damage.text = "000";
        addEnemy_movementSpeed.text = "000.00";
    }
    public void AddPlayer()
    {
        var player = new Player();
        var playerStats = new Stats(addPlayer_name.text,
                                   int.Parse(addPlayer_hp.text),
                                   int.Parse(addPlayer_damage.text),
                                   float.Parse(addPlayer_movementSpeed.text));
        player.stats = playerStats;
        worldData.playerList.Add(player);

        addPlayer_name.text = "PlayerName";
        addPlayer_hp.text = "000";
        addPlayer_damage.text = "000";
        addPlayer_movementSpeed.text = "000.00";
    }
}

[Serializable]
public class WorldData
{
    public List<Player> playerList;
    public List<Enemy> enemyList;
}
