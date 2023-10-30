using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool attacking;
    private double damage;

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 0.03f, transform.localPosition.y, transform.localPosition.z);
        }

        if (Mathf.Abs(transform.localPosition.x) > 1.85)
        {
            attacking = false;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void Attack()
    {
        damage = 30 * StaticStats.difficulty;
        StartCoroutine(AttackWait());
    }
    

    IEnumerator AttackWait()
    {
        //yield on a new YieldInstruction that waits for 0.8 seconds.
        yield return new WaitForSeconds(0.8f);
        attacking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && attacking)
        {
            StaticStats.playerHealth -= damage;
        }
    }
}
