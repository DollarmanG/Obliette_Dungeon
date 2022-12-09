using Action;
using ActionHandler;
using Movement;
using UnityEngine;

public class GuardAttack : MonoBehaviour, IAction
{
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float _range = 2f;

    private Health target;
    float timeSinceLastAttack = Mathf.Infinity;

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead()) return;

        if (!GetIsInRange(target.transform))
        {
            GetComponent<Mover>().MoveTo(target.transform.position, 1f);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackBehaviour();
        }
    }

    public Health GetTarget()
    {
        return target;
    }

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

    private void TriggerAttack()
    {
        GetComponent<Animator>().ResetTrigger("stopAttack");
        GetComponent<Animator>().SetTrigger("attack");
    }

    //Animation Event
    void Hit()
    {
        if (target == null) return;
    }

    void Shoot()
    {
        Hit();
    }

    private bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) < GetRange();
    }

    float GetRange()
    {
        return _range;
    }

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

    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }

    public void Cancel()
    {
        StopAttack();
        target = null;
        GetComponent<Mover>().Cancel();
    }

    private void StopAttack()
    {
        GetComponent<Animator>().ResetTrigger("attack");
        GetComponent<Animator>().SetTrigger("stopAttack");
    }
}
