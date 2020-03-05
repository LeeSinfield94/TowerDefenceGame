using UnityEngine;

[System.Serializable]
public class Waves
{
    public GameObject Enemy;
    //public int EnemyLevel;
    public int Lvl1EnemyAmount;
    public float Lvl1SpawnRate, Lvl1SpawnDelay;
    public int Lvl1CamoFrequency, Lvl1CamoAmount;
    public int Lvl2EnemyAmount;
    public float Lvl2SpawnRate, Lvl2SpawnDelay;
    public int Lvl2CamoFrequency, Lvl2CamoAmount;
    public int Lvl3EnemyAmount;
    public float Lvl3SpawnRate, Lvl3SpawnDelay;
    public int Lvl3CamoFrequency, Lvl3CamoAmount;
    public int Lvl4EnemyAmount;
    public float Lvl4SpawnRate, Lvl4SpawnDelay;
    public int Lvl4CamoFrequency, Lvl4CamoAmount;
    public int Lvl5EnemyAmount;
    public float Lvl5SpawnRate, Lvl5SpawnDelay;
    public int Lvl5CamoFrequency, Lvl5CamoAmount;
    public int Lvl6EnemyAmount;
    public float Lvl6SpawnRate, Lvl6SpawnDelay;
    public int Lvl6CamoFrequency, Lvl6CamoAmount;
    public int Lvl7EnemyAmount;
    public float Lvl7SpawnRate, Lvl7SpawnDelay;
    public int Lvl7CamoFrequency, Lvl7CamoAmount;
    public int Lvl8EnemyAmount;
    public float Lvl8SpawnRate, Lvl8SpawnDelay;
    public int Lvl8CamoFrequency, Lvl8CamoAmount;
    private int TotalEnemyAmount;
    public int TotalEnemies()
    {
        TotalEnemyAmount = Lvl1EnemyAmount + Lvl2EnemyAmount + Lvl3EnemyAmount + Lvl4EnemyAmount + Lvl5EnemyAmount + Lvl6EnemyAmount + Lvl7EnemyAmount + Lvl8EnemyAmount;
        return TotalEnemyAmount;
    }
}
