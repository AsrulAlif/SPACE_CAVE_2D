using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed = 10f;
    public Vector2 maxVelocity = new Vector2(3, 5);
    public bool standing;
    public float jetSpeed = 15f;
    public float airSpeedMultiplier = .3f;

    private PlayerController controller;
    private Animator animator;

    public AudioClip leftFootSound;
    public AudioClip rightFootSound;
    public AudioClip thudSound;
    public AudioClip rocketSound;

    private GameObject mainCamera;

    [Header("Game Clear Setting")]
    public GameObject gameClearText;
    private bool isGameCleared = false;

    [Header("Timer Setting")]
    public Text timerText;
    // Menggunakan static agar waktu tidak mereset saat player mati dan scene reload
    private static float elapsedTime = 0f;
    private static bool timerIsRunning = true;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        if (gameClearText != null)
        {
            gameClearText.SetActive(false);
        }

        mainCamera = Camera.main?.gameObject;

        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera);
        }

        timerIsRunning = true;
    }

    void PlayLeftFootSound()
    {
        if (leftFootSound)
            AudioSource.PlayClipAtPoint(leftFootSound, transform.position);
    }

    void PlayRightFootSound()
    {
        if (rightFootSound)
            AudioSource.PlayClipAtPoint(rightFootSound, transform.position);
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (!standing)
        {
            var absVelX = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
            var absVelY = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);

            if (absVelX <= .1f || absVelY <= .1f)
            {
                if (thudSound)
                    AudioSource.PlayClipAtPoint(thudSound, transform.position);
            }
        }
    }

    void PlayRocketSound()
    {
        if (!rocketSound || GameObject.Find("RocketSound"))
            return;

        GameObject go = new GameObject("RocketSound");
        AudioSource aSrc = go.AddComponent<AudioSource>();
        aSrc.clip = rocketSound;
        aSrc.volume = 0.7f;
        aSrc.Play();

        Destroy(go, rocketSound.length);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
        }

        if (isGameCleared)
        {
            if (Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                RestartGame();
            }
            return;
        }

        var forceX = 0f;
        var forceY = 0f;

        var absVelX = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
        var absVelY = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);

        if (absVelY < .2f)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }

        if (controller.moving.x != 0)
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier);
                transform.localScale = new Vector3(forceX > 0 ? 1 : -1, 1, 1);
            }
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (controller.moving.y > 0)
        {
            PlayRocketSound();
            if (absVelY < maxVelocity.y)
                forceY = jetSpeed * controller.moving.y;
            animator.SetInteger("AnimState", 2);
        }
        else if (absVelY > 0)
        {
            animator.SetInteger("AnimState", 3);
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY));
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        // Jika menyentuh Finish / Game Clear
        if (target.CompareTag("Finish") || target.name.Contains("ClearObject"))
        {
            ClearGame();
        }
    }

    void ClearGame()
    {
        isGameCleared = true;
        timerIsRunning = false;

        // ** TIMER DIRESET KE 0 KETIKA BERHASIL (GAME CLEAR) **
        elapsedTime = 0f;

        // Reset skor juga saat menang
        PengaturGameStatic.ResetSkor();

        if (gameClearText != null)
        {
            gameClearText.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void OnDestroy()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null;
        }
    }
}