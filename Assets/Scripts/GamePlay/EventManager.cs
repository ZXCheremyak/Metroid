using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<CheckPoint> CheckPointUsed = new UnityEvent<CheckPoint>();

    public static UnityEvent PlayerDied = new UnityEvent();

    public static UnityEvent<AudioClip> PlaySound = new UnityEvent<AudioClip>();

    public static UnityEvent<AudioClip, Vector2> PlaySoundAtLocation = new UnityEvent<AudioClip, Vector2>();
    
    public static UnityEvent<AudioClip> PlayMusic = new UnityEvent<AudioClip>();

    public static UnityEvent<EnemyParameters> EnemyDied = new UnityEvent<EnemyParameters>();

    public static UnityEvent enemyDamaged = new UnityEvent();

    public static UnityEvent Save = new UnityEvent();

    public static UnityEvent Load = new UnityEvent();

    public static UnityEvent<Parameters> BossDied = new UnityEvent<Parameters>();

    public static UnityEvent BossFightStarted = new UnityEvent();

    public static UnityEvent<float, float> ChangePlayerHealth = new UnityEvent<float, float>();

    public static UnityEvent<float, float> ChangeFlaskCharge = new UnityEvent<float, float>();

    public static UnityEvent<int> GainExp = new UnityEvent<int>();

    public static UnityEvent<float> changeMusicVolume = new UnityEvent<float>();

    public static UnityEvent<float> changeSfxVolume = new UnityEvent<float>();

    public static UnityEvent<int, int> ChangeExp = new UnityEvent<int, int>();

    public static UnityEvent lvlUp = new UnityEvent();
}
