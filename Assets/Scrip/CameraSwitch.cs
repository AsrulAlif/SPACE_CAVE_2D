using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Tentukan posisi baru yang ingin dituju oleh kamera utama
    public Transform newCameraPosition;
    private Camera mainCamera;

    void Start()
    {
        // Mengambil referensi ke kamera utama
        mainCamera = Camera.main;
    }

    // Method ini akan dijalankan ketika Player memasuki area collider trigger
    private void OnTriggerEnter(Collider other)
    {
        // Memastikan objek yang masuk adalah Player (tag harus diatur menjadi "Player")
        if (other.CompareTag("Player"))
        {
            // Memindahkan posisi kamera utama ke posisi baru yang telah ditentukan
            mainCamera.transform.position = newCameraPosition.position;
            mainCamera.transform.rotation = newCameraPosition.rotation;
        }
    }
}
