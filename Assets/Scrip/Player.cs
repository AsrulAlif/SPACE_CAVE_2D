using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Dibutuhkan untuk reload/restart scene

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
    public GameObject gameClearText; // Drag UI Text/Panel "Game Clear" ke sini lewat Inspector
    private bool isGameCleared = false;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        // Pastikan tulisan Game Clear tersembunyi saat game dimulai
        if (gameClearText != null)
        {
            gameClearText.SetActive(false);
        }

        // Cari dan simpan referensi ke Main Camera
        mainCamera = Camera.main?.gameObject;

        // Pastikan kamera tidak dihancurkan saat scene berganti atau player dihancurkan
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera);
        }
    }

    void PlayLeftFootSound()
    {
        if (leftFootSound) // jika file suaranya tidak kosong
            AudioSource.PlayClipAtPoint(leftFootSound, transform.position); // play pada posisi player
    }

    void PlayRightFootSound()
    {
        if (rightFootSound)
            AudioSource.PlayClipAtPoint(rightFootSound, transform.position);
    }

    void OnCollisionEnter2D(Collision2D target)
    { // bunyi ketika mendarat/injak tanah
        if (!standing)
        { // lagi terbang
            var absVelX = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x); // ambil posisi
            var absVelY = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);

            if (absVelX <= .1f || absVelY <= .1f)
            { // gerakan x&y kecil, artinya sdg mendarat
                if (thudSound)
                    AudioSource.PlayClipAtPoint(thudSound, transform.position);
            }
        }
    }

    void PlayRocketSound()
    {
        if (!rocketSound || GameObject.Find("RocketSound")) // jika sudah ada gameobjectnya, tidak dibunyikan lagi
            return; // langsung return

        GameObject go = new GameObject("RocketSound"); // buat gameobject
        AudioSource aSrc = go.AddComponent<AudioSource>(); // masukkan audiosource
        aSrc.clip = rocketSound; // masukkan clip
        aSrc.volume = 0.7f; // set volume
        aSrc.Play();

        Destroy(go, rocketSound.length); // destroy setelah suara selesai dibunyikan		
    }

    // Update is called once per frame
    void Update()
    {
        // Jika game sudah tamat, cek apakah pemain menekan tombol atau sentuh layar untuk restart
        if (isGameCleared)
        {
            if (Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                RestartGame();
            }
            return; // Menghentikan update player jika game sudah clear
        }

        // menetralkan nilai gerakan
        var forceX = 0f;
        var forceY = 0f;

        // ngecek kecepatan sekarang
        var absVelX = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
        var absVelY = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);

        if (absVelY < .2f)
        { // cek lagi terbang atau tidak
            standing = true;
        }
        else
        {
            standing = false;
        }

        if (controller.moving.x != 0)
        { // cek kontrol gerak kiri-kanan
            if (absVelX < maxVelocity.x)
            { // pembatas agar tidak lebih dari kecepatan maks
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier); // jika di udara akan ada perkalian dgn airspeed, yg membuatnya lbh lamban
                transform.localScale = new Vector3(forceX > 0 ? 1 : -1, 1, 1); // flip gambar
            }
            animator.SetInteger("AnimState", 1); // animasi jalan
        }
        else
        {
            animator.SetInteger("AnimState", 0); // animasi stop
        }

        if (controller.moving.y > 0)
        { // cek kontrol gerak ke atas
            PlayRocketSound();
            if (absVelY < maxVelocity.y) // pembatas kecepatan terbang
                forceY = jetSpeed * controller.moving.y;
            animator.SetInteger("AnimState", 2); // animasi jet terbang ada apinya
        }
        else if (absVelY > 0)
        {
            animator.SetInteger("AnimState", 3); // animasi jet tidak ada apinya
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY)); // melakukan gerakan berdasarkan nilai forceX & forceY yg sudah dihitung di atas
    }

    // Fungsi mendeteksi objek Game Clear (Trigger)
    private void OnTriggerEnter2D(Collider2D target)
    {
        // Cek apakah objek yang disentuh memiliki tag "Finish" atau nama tertentu
        // Kamu bisa ubah "Finish" sesuai keinginan atau cek berdasarkan komponen
        if (target.CompareTag("Finish") || target.name.Contains("ClearObject"))
        {
            ClearGame();
        }
    }

    void ClearGame()
    {
        isGameCleared = true;

        // Tampilkan teks Game Clear
        if (gameClearText != null)
        {
            gameClearText.SetActive(true);
        }

        // Hentikan pergerakan game/waktu sementara
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        // Kembalikan waktu ke normal sebelum merestart scene
        Time.timeScale = 1f;

        // Load ulang scene yang aktif saat ini
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void OnDestroy()
    {
        // Jika player dihancurkan, pastikan Main Camera dilepas dari player agar tidak ikut hancur
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null;
        }
    }
}