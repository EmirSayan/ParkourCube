using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    public Animator handAnimator;
    private GameManager gameManager;
    private AudioManager audioManager;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private CheckPointSystem checkPointSystem;
    public GameObject gameOverCanvas;
    public GameObject newLevelCanvas;

    void Start()
    {       
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        
        checkPointSystem = FindObjectOfType<CheckPointSystem>();

        if (checkPointSystem != null)
        {
            transform.position = checkPointSystem.GetCurrentCheckPointLocation();
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(handAnimator != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                handAnimator.SetTrigger("isTapped");
                Invoke("FixedHandDuration", 0.1f);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            checkPointSystem.DestroyCheckPointSystem();
            
            GameManager.GoNextLevel(0);
            PlayerMovment.Die();
        }
    }

    private void FixedHandDuration()
    {
        handAnimator.ResetTrigger("isTapped");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            audioManager.Play("CheckPoint");
            GameManager.SetIsNewLevel(false);
            checkPointSystem.SetCurrentCheckPointLocation(other.transform.position);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("FakeCube"))
        {
            audioManager.Play("Fake");
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Portal"))
        {
            audioManager.Play("Portal");
            PlayerMovment.Die();
            int scnNum = other.GetComponent<Portal>().GetSceneNumber();
            LevelManager.Instance.UnlockLevel(scnNum);
            LevelManager.Instance.SaveLevel(scnNum);
            checkPointSystem.DestroyCheckPointSystem();
            StartCoroutine(NewLevelCanvas(scnNum));




        }
        if (other.gameObject.CompareTag("Lava"))
        {
            audioManager.Play("Die");
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            capsuleCollider.isTrigger = true;
            PlayerMovment.Die();

            gameManager.PlayerDie();
            StartCoroutine(GameOverCanvas());
        }
    }

    IEnumerator GameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        gameOverCanvas.SetActive(false);
    }

    IEnumerator NewLevelCanvas(int scnNum)
    {

        newLevelCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        GameManager.GoNextLevel(scnNum);
    }
}
