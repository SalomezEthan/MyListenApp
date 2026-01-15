namespace MyArchitecture.ApplicationLayer.Listener
{
    public abstract class Listener<T> where T : class
    {
        public event EventHandler<T>? Notified;
        protected void OnNotified(T notification)
        {
            Notified?.Invoke(this, notification);
        }
    }

    public abstract class  Listener 
    {
        public event EventHandler? Notified;
        protected void OnNotified()
        {
            Notified?.Invoke(this, EventArgs.Empty);
        }
    }
}
