using UnityEngine;
using UnityEngine.InputSystem;

public class ShootSystem : MonoBehaviour
{
    public float shootForce = 20f;
    public Transform firePoint;
    public GameObject waterProj;
    public float fireRate = .2f;

    private InputAction shootAction;

    public ParticleSystem waterSplash;
    public AudioClip waterSound;

    private float nextFireTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    
    void Update()
    {
        if (shootAction.WasPressedThisFrame() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject waterBullet = Instantiate(waterProj, firePoint.position, firePoint.rotation);
        Rigidbody rb = waterBullet.GetComponent<Rigidbody>();

        // Добавляем случайное отклонение для "водяного" эффекта
        Vector3 direction = firePoint.forward +
                           Random.insideUnitSphere * 0.05f;

        rb.AddForce(direction * shootForce, ForceMode.Impulse);

        // Эффекты
        if (waterSplash != null)
            Instantiate(waterSplash, firePoint.position, firePoint.rotation);

        if (waterSound != null)
            audioSource.PlayOneShot(waterSound);
    }
}
