using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public Transform gunPoint;
    public GameObject gunShot;
    public float shootingTimeInterval;
    public float shootingDistance;
    public float followDistance;
    public int health;
    public int hitAmount;
    public GameObject explosion;

    private Vector3 currentWatchPointToGo;
    private NavMeshAgent navAgent;
    private Transform player;

    private float initialTimeShoot;
    private bool canShoot;
    private bool isDead;
    private AudioSource[] audioSources;
    private Transform[] watchPoints;

    // Use this for initialization
    void Start () {
        explosion.SetActive(false);
        GetWatchPoints();
        currentWatchPointToGo = watchPoints[Random.Range(0, 5)].position;
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        canShoot = true;
        isDead = false;
        audioSources = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!isDead)
        {
            if (!GameManager.playerIsDead && Vector3.Distance(transform.position, player.position) <= followDistance)
            {
                FollowPlayerMode();
            }
            else
            {
                WatchMode();
            }
        }
        
        
	}

    void WatchMode()
    {
        if(navAgent.velocity == Vector3.zero)
        {
            currentWatchPointToGo = watchPoints[Random.Range(0, 5)].position;
        }

        navAgent.SetDestination(currentWatchPointToGo);
        transform.LookAt(new Vector3(currentWatchPointToGo.x, transform.position.y, currentWatchPointToGo.z));
    }

    void FollowPlayerMode()
    {
        navAgent.SetDestination(player.position);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if(canShoot)
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

    void PlayShootSound()
    {
        audioSources[1].Play();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunPoint.transform.position, transform.forward, out hit, shootingDistance))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                //Hit player
                PlayShootSound();
                GameObject go = GameObject.Instantiate(gunShot, gunPoint.position, gunPoint.rotation) as GameObject;
                GameObject.Destroy(go, 1f);
                player.SendMessage("TakeHit", hitAmount);
            }
        }
        canShoot = false;
    }

    void Die()
    {
        isDead = true;
        explosion.SetActive(true);
        navAgent.Stop();
        Destroy(gameObject, 5);
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

    void GetWatchPoints()
    {
        watchPoints = new Transform[5];
        int i = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Point"))
        {
            watchPoints[i] = go.transform;
            i++;
        }
    }
}
