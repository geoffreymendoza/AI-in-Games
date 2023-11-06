using UnityEngine;

namespace CubeChan {
public static class Data {
    public static readonly int IDLE_ANIMATION = Animator.StringToHash("Def_Idle");
    public static readonly int RUN_ANIMATION = Animator.StringToHash("Def_Run");
    public static readonly int THROW_ANIMATION = Animator.StringToHash("Def_Throw");
    public static readonly int DEATH_ANIMATION = Animator.StringToHash("Def_Death");
    public static readonly int PICKUP_ANIMATION = Animator.StringToHash("Def_Pickup");
    public static readonly int NAVIGATION = Animator.StringToHash("Navigation");
    public const string NAVIGATION_ANIMATION = "nav_speed";
}

public enum Team {
    Neutral,
    Red,
    Green
}

public enum GameState {
    Invalid,
    Dodging,
    Throwing,
}
}