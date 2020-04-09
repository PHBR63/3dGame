using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IAControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;

    public NavMeshAgent agent;

    public Animator anim;

    public enum States
    {
        idle,
        berserk,
        attack,
        die,
        damage,
        patrol,

    }
    public States state;
    void Start()
    {
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {

      

        anim.SetFloat("Velocidade", agent.velocity.magnitude);
    }

    void ChangeState()
    {
        switch (state)
        {
            case States.idle:
                StartCoroutine(Idle());
                break;
            case States.berserk:
                StartCoroutine(Berserk());
                break;
            case States.attack:
                StartCoroutine(Attack());
                break;
            case States.die:
                StartCoroutine(Die());
                break;
            case States.damage:
                StartCoroutine(Damage());
                break;
            case States.patrol:
                StartCoroutine(Patrol());
                break;
        }
    }

    private IEnumerator Patrol()
    {
        Vector3 newplace = new Vector3(transform.position.x + UnityEngine.Random.Range(-10, 10), transform.position.y, transform.position.z + UnityEngine.Random.Range(-10, 10));
        agent.isStopped = false;

        float waitingtime = UnityEngine.Random.Range(1, 5);
        while (state == States.patrol)
        {
            
            agent.SetDestination(newplace);
            yield return new WaitForEndOfFrame();

            if (Vector3.Distance(transform.position, target.transform.position) < 1)
            {
                ChangeState(States.idle);
            }
            waitingtime -= Time.deltaTime;
            if (waitingtime < 0)
            {
                ChangeState(States.idle);
            }
        }
        ChangeState();
    }

    private IEnumerator Damage()
    {
        agent.isStopped = true;
        anim.SetBool("Dano", true);
        while (state == States.damage)
        {

            yield return new WaitForSeconds(.5f);
            ChangeState(States.idle);
        }
        anim.SetBool("Dano", false);
        ChangeState();
    }

    private IEnumerator Attack()
    {
        agent.isStopped = true;
        anim.SetBool("Atacando", true);
        while (state == States.attack)
        {
           
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.position, target.transform.position) > 3)
            {
                ChangeState(States.berserk);
            }
        }
        ChangeState();
        anim.SetBool("Atacando", false);
    }

    void ChangeState(States mystate)
    {
        state = mystate;
    }

    IEnumerator Idle()
    {
        agent.isStopped = true;
        float waitingtime = UnityEngine.Random.Range(1, 5);
        while (state == States.idle)
        {
           
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.position, target.transform.position) < 20)
            {
                ChangeState(States.berserk);
            }
            waitingtime -= Time.deltaTime;
            if (waitingtime <0)
            {
                ChangeState(States.patrol);
            }
        }
        ChangeState();
    }
    IEnumerator Berserk()
    {
        agent.isStopped = false;
        while (state == States.berserk)
        {
            print("atacando");
            agent.SetDestination(target.transform.position);
            yield return new WaitForEndOfFrame();

            if (Vector3.Distance(transform.position, target.transform.position) < 2)
            {
                ChangeState(States.attack);
            }
        }
        ChangeState();
    }

    IEnumerator Die()
    {
        agent.isStopped = true;
        anim.SetBool("Morrendo", true);
        Destroy(gameObject, 5);
       
        yield return new WaitForEndOfFrame();

    }
}
