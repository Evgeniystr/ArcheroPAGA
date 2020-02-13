public abstract class Movement
{
    public abstract float moveSpeed { get;  set; }
    public abstract bool inProgress { get; set; }


    public abstract void DoMove();

    public void SpeedUpdate(float value)
    {
        moveSpeed = value;
    }
}
