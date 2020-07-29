using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// A block
/// </summary>
public class Block : MonoBehaviour
{
    protected int points;
    PointsAddedEvent pointsAddedEvent;

    BlockDestroyedEvent blockDestroyedEvent;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    virtual protected void Start()
    {
        pointsAddedEvent = new PointsAddedEvent();
        EventManager.AddPointAddedEventInvoker(this);

        blockDestroyedEvent = new BlockDestroyedEvent();
        EventManager.AddLastBlockDestroyedEventInvoker(this);

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    public void AddPointsAddedEffectListener(UnityAction<int> listener)
    {
        pointsAddedEvent.AddListener(listener);
    }

    public void AddLastBlockDestroyedEventListener(UnityAction listener)
    {
        blockDestroyedEvent.AddListener(listener);
    }

    /// <summary>
    /// Destroys the block on collision with a ball
    /// </summary>
    /// <param name="coll">Coll.</param>
    virtual protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            // HUD.AddPoints(points);
            pointsAddedEvent.Invoke(points);
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            if (blocks.Length == 1)
            {
                blockDestroyedEvent.Invoke();
            }
            //AudioManager.Play(AudioClipName.BlockHit);
            Destroy(gameObject);
        }
    }
}
