using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;  // Arrastra aquí tu panel de Victoria (UI)
    public GameObject losePanel; // Arrastra aquí tu panel de Derrota (UI)

    public int enemyCount;
    private bool gameEnded = false;

    void Start()
    {
        // Cuenta automáticamente cuántos enemigos hay en la escena al empezar
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Aseguramos que los paneles estén ocultos al inicio
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void EnemyDefeated()
    {
        if (gameEnded) return;

        enemyCount--;

        // Condición de Victoria
        if (enemyCount <= 0)
        {
            WinGame();
        }
    }

    public void PlayerDied()
    {
        if (gameEnded) return;
        LoseGame();
    }

    void WinGame()
    {
        gameEnded = true;

        // Liberar el mouse para poder hacer click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void LoseGame()
    {
        gameEnded = true;

        // Liberar el mouse para poder hacer click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Llama a esta función desde un botón en tu UI para reiniciar
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reactivar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Llama a esto desde el botón "Salir"
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego..."); // Para que veas en la consola que funciona
        Application.Quit();
    }
}