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

      

        // anim.SetFloat("Velocidade", agent.velocity.magnitude);
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
        throw new NotImplementedException();
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
        while (state == States.idle)
        {
           
            yield return new WaitForEndOfFrame();

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
