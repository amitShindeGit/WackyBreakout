using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    // move delay timer
    Timer moveTimer;

    // death timer
    Timer deathTimer;

    // speedup effect support
    Rigidbody2D rb2d;
    Timer speedupTimer;
    float speedupFactor;

    BallsLeftEvent ballsLeftEvent;
    DeathBallEvent deathballevent;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // start move timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.AddTimerFinishedListener(HandleMoveTimerFinishedEvent);
        moveTimer.Duration = 1;
        moveTimer.Run();

        // start death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifeSeconds;
        deathTimer.AddTimerFinishedListener(HandleDeathTimerFinishedEvent);
        deathTimer.Run();

        // speedup effect support
        speedupTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedupEffectListener(HandleSpeedupEffectActivatedEvent);
        speedupTimer.AddTimerFinishedListener(HandleSpeedUpTimerFinishedEvent);
        rb2d = GetComponent<Rigidbody2D>();

        ballsLeftEvent = new BallsLeftEvent();
        EventManager.AddBallsLeftEffectInvoker(this);

        deathballevent = new DeathBallEvent();
        EventManager.AddDeathBallEventInvoker(this);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
	{
      

    }

    // move when time is up
    void HandleMoveTimerFinishedEvent()
    {
        moveTimer.Stop();
        StartMoving();
    }

    // die when time is up
    void HandleDeathTimerFinishedEvent()
    {
        // spawn new ball and destroy self
        //Camera.main.GetComponent<BallSpawner>().SpawnBall();
        deathballevent.Invoke();
        Destroy(gameObject);
    }

     // return to normal speed as appropriate
     //if (speedupTimer.Finished)
     void HandleSpeedUpTimerFinishedEvent()
     {
            speedupTimer.Stop();
            rb2d.velocity *= 1 / speedupFactor;
     }

    /// <summary>
    /// Spawn new ball and destroy self when out of game
    /// </summary>
    void OnBecameInvisible()
    {
        // death timer destruction is in Update
        //if (!deathTimer.Finished)
        if(deathTimer.Running) //scam
        {
            // only spawn a new ball if below screen
            float halfColliderHeight = 
                gameObject.GetComponent<BoxCollider2D>().size.y / 2;
            if (transform.position.y - halfColliderHeight < ScreenUtils.ScreenBottom)
            {
                //Camera.main.GetComponent<BallSpawner>().SpawnBall();
                // HUD.ReduceBallsLeft();
                ballsLeftEvent.Invoke();
                AudioManager.Play(AudioClipName.BallLost);
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts the ball moving
    /// </summary>
    public void StartMoving()
    {
        // calculate force to apply
        float angle = -90 * Mathf.Deg2Rad;
        Vector2 force = new Vector2(
            ConfigurationUtils.BallImpulseForce * Mathf.Cos(angle),
            ConfigurationUtils.BallImpulseForce * Mathf.Sin(angle));

        // adjust as necessary if speedup effect is active
        if (EffectUtils.SpeedupEffectActive)
        {
            StartSpeedupEffect(EffectUtils.SpeedupEffectSecondsLeft,
                EffectUtils.SpeedupFactor);
            force *= speedupFactor;
        }

        // get ball moving
        GetComponent<Rigidbody2D>().AddForce(force);
    }

    /// <summary>
    /// Sets the ball direction to the given direction
    /// </summary>
    /// <param name="direction">direction</param>
    public void SetDirection(Vector2 direction)
    {
        // get current rigidbody speed
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        float speed = rb2d.velocity.magnitude;
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Handles the speedup effect activated event
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup factor</param>
    void HandleSpeedupEffectActivatedEvent(float duration, float speedupFactor)
    {
        // speed up ball and run or add time to timer
        if (!speedupTimer.Running)
        {
            StartSpeedupEffect(duration, speedupFactor);
            rb2d.velocity *= speedupFactor;
        }
        else
        {
            speedupTimer.AddTime(duration);
        }
    }

    /// <summary>
    /// Starts the speedup effect
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup factor</param>
    void StartSpeedupEffect(float duration, float speedupFactor)
    {
        this.speedupFactor = speedupFactor;
        speedupTimer.Duration = duration;
        speedupTimer.Run();
    }

    //AddListeners method for ballsLeft
    public void AddBallsLeftEfectListener(UnityAction listener)
    {
        ballsLeftEvent.AddListener(listener);
    }

    public void AddDeathBallEventListener(UnityAction listener)
    {
        deathballevent.AddListener(listener);
    }
}