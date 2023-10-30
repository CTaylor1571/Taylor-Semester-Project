using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 85f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.12f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    private int health;
    private int maxHealth;
    private bool attackable;
    private bool damageByTouch = false;
    private bool moveable;
    private Vector2 moveDirection;


    [SerializeField]
    bool isGrounded = false;

    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField]
    GameObject bottom;

    [SerializeField]
    GameObject attack;

    [SerializeField]
    private Sprite[] sprites;

    GameObject gameMan;

    public void Start()
    {
        attackable = true;
        damageByTouch = true;
        moveable = true;
        //attack.SetActive(false);
        gameMan = GameObject.Find("GameManager");
        target = GameObject.Find("Player").transform;
        health = (int)(100 * StaticStats.difficulty);
        maxHealth = health;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
        if (StaticStats.teleporterActive)
        {
            activateDistance = 400f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health < 1)
        {
            return;
        }
        if (other.name.IndexOf("Pellet") != -1) 
        {
            if (other.gameObject.GetComponent<PelletBehavior>().isCritical)
            {
                health -= 12;
            }
            Destroy(other.gameObject);
            health -= 12;
            if (health < 1)
            {
                Debug.Log("Enemy Slain");
                Destroy(gameObject);
                gameMan.GetComponent<GameManagerScript>().Earn((int)(65 + ( 65 * 3 * (StaticStats.difficulty-1)/4)));
                StaticStats.numEnemies--;
                Debug.Log("Num enemies remaining: " + StaticStats.numEnemies);
            }
        }
        if (health <= maxHealth*2.0/3.0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (health <= maxHealth/3.0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && damageByTouch)
        {
            StaticStats.playerHealth-=0.75;
        }
    }

    private void UpdatePath()
    {
       if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        } 
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        //Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //See if colliding with anything
        RaycastHit2D hit = Physics2D.Raycast(bottom.transform.position, -Vector3.up, jumpCheckOffset); //, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset
        if (hit.collider != null && (hit.collider.name.Equals("Ground") || hit.collider.name.IndexOf("Enemy") != -1)) 
        {

            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }

        //Direction calculation
        if (((Vector2)path.vectorPath[currentWaypoint]).x - rb.position.x > 0f)
        {
            moveDirection = Vector2.right;
        }
        else
        {
            moveDirection = Vector2.left;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);


        if (attackable && Vector2.Distance(transform.position, target.transform.position) < 3f)                                             // attacking logic
        {
            Attack();
        }


        direction = direction.normalized;
        Vector2 force = moveDirection * speed * Time.deltaTime;
        if (!moveable)
        {
            force = Vector2.zero;
        }

        //Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement || (direction.x > 4f && rb.velocity.x < 0.01))                            // remove second part if needed
            {
                rb.AddForce(Vector2.up * jumpModifier *2, ForceMode2D.Impulse);
            }
        }

        //Movement
        rb.AddForce(force);

        //Next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        //Direction graphics handling
        if (directionLookEnabled)
        {
            if (direction.x > 0f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x <0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Attack()
    {
        attackable = false;
        moveable = false;
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        // do attack
        //attack.SetActive(true);
        GetComponentInChildren<EnemyAttack>().Attack();

        // set timer before attacking again
        StartCoroutine(AttackWait());
    }
    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(2);
        moveable = true;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        yield return new WaitForSeconds(2);
        attackable = true;
    }
}
