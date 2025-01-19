using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    private static bool isNewLevel;
    private static UiManager uiManager;
    [SerializeField] static float respawnTime = 2f;
    private bool sesGeliyor = true;
    public TextMeshProUGUI youDiedText;
    private static AudioManager audioManager;

    private void Awake()
    {       
        ManageSingleton();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            isNewLevel = false;

            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
    }

    public static void SetIsNewLevel(bool value)
    {
        isNewLevel = value;
    }
    public static bool GetIsNewLevel()
    { return isNewLevel; }

    public static void GoNextLevel(int buildIndex)
    {
        SetIsNewLevel(true);

        if (buildIndex == 0)
        {
            audioManager.Play("MenuMusic");
            audioManager.Stop("GameMusic");
        }
        else
        {
            audioManager.Stop("MenuMusic");
        }

        SceneManager.LoadScene(buildIndex);      
    }
    
    public void PlayerDie()
    {
        uiManager.RedDeadScreen();
        Invoke("RestartScene",respawnTime);
        
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void resetUiManager()
    {
        uiManager = FindObjectOfType<UiManager>();
    }
    public void Audio()
    {
        sesGeliyor = !sesGeliyor;
        if (sesGeliyor == true)
        {
            AudioListener.volume = 1;
        }
        else if(sesGeliyor == false)
        {
            AudioListener.volume = 0;
        }
    }
    

}
