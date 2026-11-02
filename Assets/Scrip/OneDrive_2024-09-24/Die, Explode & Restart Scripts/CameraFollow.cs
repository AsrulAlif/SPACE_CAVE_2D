using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Masukkan objek Player ke sini
    public float smoothing = 5f; // Kecepatan kelancaran kamera mengikuti player
    public Vector3 offset;       // Jarak posisi kamera dari player (misal: Z harus -10 agar tidak menutupi player)

    void Start()
    {
        // Jika offset belum diatur, gunakan posisi awal kamera terhadap target
        if (target != null && offset == Vector3.zero)
        {
            offset = transform.position - target.position;
            offset.z = -10f; //kamera berada di depan layar (Z = -10 untuk 2D)
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // posisi tujuan kamera
            Vector3 targetCamPos = target.position + offset;

            // Gerakkan kamera secara mulus menuju posisi player
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        else
        {
            // Jika player hancur (mati), cari player baru secara otomatis di scene (opsional saat respawn/restart)
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }
    }
}