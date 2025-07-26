using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerCollector : MonoBehaviour
{
    public int totalItems = 5;
    public float timeLimit = 60f;
    public TMP_Text timerText;
    public TMP_Text collectedText;
    public GameObject panelVictoire;
    public GameObject panelDefaite;


    private int collected = 0;
    private float timer;

    void Start()
    {
        Debug.Log("TimerText = " + (timerText != null));
        Debug.Log("CollectedText = " + (collectedText != null));

        timer = timeLimit;
        UpdateUI();

    }

    void Update()
    {
        if (collected >= totalItems)
        {
            ShowVictoryPanel();
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;

        UpdateUI();

        if (timer <= 0)
        {
            ShowDefeatPanel();
        }
    }
    void ShowVictoryPanel()
    {
        if (panelVictoire != null)
            panelVictoire.SetActive(true);
        Time.timeScale = 0f; // stoppe le jeu
    }

    void ShowDefeatPanel()
    {
        if (panelDefaite != null)
            panelDefaite.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // à adapter
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            collected++;
            Destroy(other.gameObject);
            UpdateUI();
        }
    }

    void UpdateUI()

    {

        if (timerText != null)
            timerText.text = "Temps restant : " + Mathf.Ceil(timer).ToString();
        if (collectedText != null)
            collectedText.text = "Objets : " + collected + "/" + totalItems;

    }
}
