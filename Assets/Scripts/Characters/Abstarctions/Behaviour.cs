using UnityEngine;

public enum BehavoiurState
{
    Move,
    Attack
}

public abstract class Behaviour : MonoBehaviour
{
    public abstract Movement movement { get; set; }
    public abstract Attack attack { get; set; }

    protected BehavoiurState behavoiurState;

    /// <summary>
    /// Handle behaviour of character
    /// </summary>
    /// <remarks>
    /// Use in update
    /// </remarks>
    public virtual void BehavoiurExecute()
    {
        switch (behavoiurState)
        {
            case BehavoiurState.Move:
                if (movement.inProgress)
                    movement.DoMove();
                else
                {
                    behavoiurState = BehavoiurState.Attack;
                    attack.inProgress = true;
                }
                break;

            case BehavoiurState.Attack:
                if (attack.inProgress)
                    attack.DoAttack();
                else
                {
                    behavoiurState = BehavoiurState.Move;
                    movement.inProgress = true;
                }
                break;
        }
    }
}
