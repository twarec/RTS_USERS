namespace YG_EventSystem {
    public interface IGameObjectEvent {
        System.Action action { get; set; }
        void Active ();
    }
    public interface IGameObjectEvent<P> {
        System.Action<P> action { get; set; }
        void Active ();
    }
    public interface IGameObjectEvent<P1, P2> {
        System.Action<P1, P2> action { get; set; }
        void Active ();
    }
    public interface IGameObjectEvent<P1, P2, P3> {
        System.Action<P1, P2, P3> action { get; set; }
        void Active ();
    }
}