using Action;
using ActionHandler;
using Movement;
using UnityEngine;

public class GuardAttack : MonoBehaviour, IAction
{
    //these are future features that will make a character able to have a cooldown between attacks and the range from where it attacks.
    
    /*[SerializeField]*/ private float timeBetweenAttacks = 1f;
    /*[SerializeField]*/ private float _range = 2f;

    //this is neccessary because the target is set on the health script, but this is also a future feature that will enable characters to have health.
    private Health target;
    float timeSinceLastAttack = Mathf.Infinity;

    void Update()
    {
        
        timeSinceLastAttack += Time.deltaTime;
        //these are also future features that checks if the target is dead then it doesn't get stuck, but moves to the next action. 
        if (target == null) return;
        if (target.IsDead()) return;

        //makes the character move towards its foe and runs towards him if he is not in range.
        if (!GetIsInRange(target.transform))
        {
            GetComponent<Mover>().MoveTo(target.transform.position, 1f);
        }
        //future feature - if its close enough then it cancels the move behaviour and starts attacking.
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
    }

    //just gets the target.
    public Health GetTarget()
    {
        return target;
    }

    //future feature - makes the character look towards its target while its attacking
    private void AttackBehaviour()
    {
        transform.LookAt(target.transform);
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            //this will trigger the Hit() event.
            TriggerAttack();
            timeSinceLastAttack = 0;
        }
    }

    ////future feature - triggers the animation of attacking.
    private void TriggerAttack()
    {
        GetComponent<Animator>().ResetTrigger("stopAttack");
        GetComponent<Animator>().SetTrigger("attack");
    }

    //future feature the Animation Event happens here.
    void Hit()
    {
        if (target == null) return;
    }

    //future feature the hit animation triggers here
    void Shoot()
    {
        Hit();
    }

    //gets the range from the target
    private bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) < GetRange();
    }

    //gets the range
    float GetRange()
    {
        return _range;
    }
    //future feature
    //this method checks if it can or can't attack the target
    public bool CanAttack(GameObject combatTarget)
    {
        if (combatTarget == null) return false;
        if (!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) &&
            !GetIsInRange(combatTarget.transform))
        {
            return false;
        }
        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }
    //future feature
    //attacks the target and starts the action
    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }
    //future feature
    //cancels the attack action
    public void Cancel()
    {
        StopAttack();
        target = null;
        GetComponent<Mover>().Cancel();
    }

    //future feature
    //stops the animation of attacking
    private void StopAttack()
    {
        GetComponent<Animator>().ResetTrigger("attack");
        GetComponent<Animator>().SetTrigger("stopAttack");
    }
}
