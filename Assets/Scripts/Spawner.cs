using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public float minSpawnInterval = 3f; // Intervalo m�nimo de tiempo entre spawns en segundos
    public float maxSpawnInterval = 5f; // Intervalo m�ximo de tiempo entre spawns en segundos
    private float timer; // Temporizador para contar el tiempo
    private float nextSpawnTime; // Tiempo del pr�ximo spawn

    void Start()
    {
        // Inicializar el temporizador y el tiempo del pr�ximo spawn
        SetNextSpawnTime();
    }

    void Update()
    {
        // Contar el tiempo
        timer -= Time.deltaTime;

        // Si el temporizador llega a 0, spawnear un enemigo y reiniciar el temporizador
        if (timer <= 0f)
        {
            SpawnEnemy();
            SetNextSpawnTime();
        }
    }

    void SpawnEnemy()
    {
        // Instanciar un enemigo en la posici�n y rotaci�n del spawner
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    void SetNextSpawnTime()
    {
        // Establecer el siguiente tiempo de spawn a un valor aleatorio entre minSpawnInterval y maxSpawnInterval
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        timer = nextSpawnTime;
    }
}
