using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    public Transform PLAYER_BASE; // Asigna el objeto PLAYER_BASE en el inspector o dinámicamente
    public float speed = 5f; // Velocidad de movimiento del enemigo
    public float followDistance = 1.5f; // Distancia mínima para que el enemigo deje de acercarse demasiado

    private bool isPlayerTrapped = false;
    private bool isGameStarted = false;

    void Start()
    {
        if (PLAYER_BASE == null)
        {
            // Buscar dinámicamente al jugador
            GameObject playerBaseObject = GameObject.Find("PLAYER_BASE");
            if (playerBaseObject != null)
            {
                PLAYER_BASE = playerBaseObject.transform;
            }
            else
            {
                Debug.LogError("PLAYER_BASE no encontrado en la escena. Asegúrate de que exista y tenga el nombre correcto.");
            }
        }
    }

    void Update()
    {
        if (!isGameStarted || PLAYER_BASE == null) return; // Esperar hasta que el juego inicie

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, PLAYER_BASE.position);

        if (distanceToPlayer > followDistance || isPlayerTrapped)
        {
            // Seguir al jugador si está lejos o si está atrapado
            Vector3 direction = (PLAYER_BASE.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("jugadorp"))
        {
            
            
                Debug.Log("Game Over: Enemy atrapó a PLAYER_BASE.");
                SceneManager.LoadScene("GAME_OVER_SCREEN");
            
        }
    }

    public void StartGame()
    {
        isGameStarted = true; // Activa el seguimiento cuando el juego comienza
    }

    public void TrapPlayer()
    {
        isPlayerTrapped = true; // Indica que el jugador está atrapado
    }
}
