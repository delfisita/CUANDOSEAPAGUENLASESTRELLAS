using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CheckpointLoadNext : MonoBehaviour
{
    [Header("Qui�n activa")]
    public string playerTag = "Player";

    [Header("Destino")]
    public bool useBuildIndexOrder = true;     // true = escena siguiente en Build Settings
    public string nextSceneName = "";          // si no us�s �ndice, pon� el nombre exacto

    [Header("Opcional")]
    public float loadDelay = 0.5f;             // peque�a pausa antes de cargar
    public Animator anim;                      // anim del checkpoint (Trigger "Activate")
    public AudioSource sfx;                    // sonido opcional
    bool triggered;

    void Awake()
    {
        // Asegur� isTrigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag(playerTag)) return;
        triggered = true;

        if (anim) anim.SetTrigger("Activate");
        if (sfx) sfx.Play();

        StartCoroutine(LoadNextRoutine());
    }

    IEnumerator LoadNextRoutine()
    {
        yield return new WaitForSeconds(loadDelay);

        if (useBuildIndexOrder)
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(i + 1);
        }
        else
        {
            if (string.IsNullOrEmpty(nextSceneName))
            {
                Debug.LogError("[CheckpointLoadNext] nextSceneName vac�o.");
                yield break;
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

