using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WayController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public int idDestinationPoint = 0;
    public float maxEnemyDistance = 5;
    private float nearestEnemyDistance;
    public Transform[] points;
    public GameObject[] enemies;
    private bool isOnPoint, isNoEnemies;
    public Animator animator;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyPoint");
        navAgent.isStopped = true;
        transform.position = points[0].position;
        navAgent.destination = points[0].position;
        nearestEnemyDistance = Mathf.Infinity;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            navAgent.isStopped = false;
            animator.SetBool("isRun", true);
        }

        if(navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            isOnPoint = true;
            animator.SetBool("isRun", false);
        }

        //
        float prevEnemyDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            int killedEnemies = 0;

            if (enemy != null)
            {
                float currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (currentEnemyDistance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = currentEnemyDistance;
                }
                prevEnemyDistance = currentEnemyDistance;
            }
            else
            {
                nearestEnemyDistance = prevEnemyDistance;
                killedEnemies++;
            }

            if (killedEnemies == enemies.Length)
            {
                isNoEnemies = true;
            }
        }

        if (isOnPoint == true && (nearestEnemyDistance > maxEnemyDistance || isNoEnemies == true))
        {
            isOnPoint = false;
            if(idDestinationPoint < points.Length - 1)
            {
                idDestinationPoint++;
                navAgent.SetDestination(points[idDestinationPoint].position);
                if (idDestinationPoint > 1)
                {
                    animator.SetBool("isRun", true);
                }
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
