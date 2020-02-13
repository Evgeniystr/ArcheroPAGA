public abstract class Movement
{
    public abstract IMoveSettings moveSettings { get; set; }
    
    public abstract bool inProgress { get; set; }


    public abstract void DoMove();

    public void SpeedUpdate(float value)
    {
        moveSettings.MoveSpeed = value;
    }
}
