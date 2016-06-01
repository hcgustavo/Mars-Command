using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
    [SerializeField] private int firePower;
    [SerializeField] private int health;
    [SerializeField] private int shootingDistance;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform turretGun;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject gunShot;
    [SerializeField] private float shootingTimeInterval;
    [SerializeField] private GameObject explosion;

    private AudioSource[] audioSources;
    private Transform target;
    private float initialTimeShoot;
    private bool canShoot;
    private bool isDead;

    // Use this for initialization
    void Start () {
        explosion.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioSources = GetComponents<AudioSource>();
        canShoot = true;
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(!isDead)
        {
            turretGun.LookAt(new Vector3(target.position.x, turretGun.position.y, target.position.z));

            if (!GameManager.playerIsDead && canShoot && Vector3.Distance(transform.position, target.position) <= shootingDistance)
            {
                Shoot();
                initialTimeShoot = Time.time;
            }
            else
            {
                if (Time.time - initialTimeShoot >= shootingTimeInterval)
                    canShoot = true;
            }
        }
        
	}

    void PlayShootSound()
    {
        audioSources[0].Play();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunPoint.transform.position, gunPoint.transform.forward, out hit, shootingDistance))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                //Hit player
                PlayShootSound();
                GameObject go = GameObject.Instantiate(gunShot, gunPoint.position, gunPoint.rotation) as GameObject;
                GameObject.Destroy(go, 1f);
                hit.collider.gameObject.SendMessage("TakeHit", firePower);
            }
        }
        canShoot = false;
    }

    void Die()
    {
        isDead = true;
        explosion.SetActive(true);
        Destroy(gameObject, 10);
    }

    public void TakeHit(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Dies
            Die();
        }
    }
}
