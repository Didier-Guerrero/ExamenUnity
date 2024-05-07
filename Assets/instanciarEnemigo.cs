using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciarEnemigo : MonoBehaviour
{
    public GameObject enemyPrefab; // Variable para asignar el prefab en el inspector de Unity
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // Posición inicial para instanciar el prefab

    void Start()
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Instancia el prefab en una posición específica
    }
}
