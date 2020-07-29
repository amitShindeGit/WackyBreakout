using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event manager
/// </summary>
public static class EventManager
{
    // FreezerEffectActivated support
    static List<PickupBlock> freezerEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> freezerEffectListeners = 
        new List<UnityAction<float>>();

    // speedupEffectActivated support
    static List<PickupBlock> speedupEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float, float>> speedupEffectListeners = 
        new List<UnityAction<float, float>>();

    // PointsAddedEvent support
    static List<Block> pointsAddedEventInvokers = new List<Block>();
    static List<UnityAction<int>> pointsAddedEventListeners = new List<UnityAction<int>>();

    //ballsleftevent support
    static List<Ball> ballsLeftEventInvokers = new List<Ball>();
    static List<UnityAction> ballsLeftEventListener = new List<UnityAction>();

    //deathballsevent support
    static List<Ball> deathBallEventInvokers = new List<Ball>();
    static List<UnityAction> deathBallEventListener = new List<UnityAction>();
    #region Public methods

    //LastBallLostEvent Support
    static List<HUD> lastBallLostEventInvokers = new List<HUD>();
    static List<UnityAction> lastBallLostEventListeners = new List<UnityAction>();

    //LastBlockDestroyedEvent Support
    static List<Block> lastBlockDestroyedEventInvokers = new List<Block>();
    static List<UnityAction> lastBlockDestroyedEventListeners = new List<UnityAction>();


    /// <summary>
    /// Adds the given script as a freezer effect invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddFreezerEffectInvoker(PickupBlock invoker)
    {
        // add invoker to list and add all listeners to invoker
        freezerEffectInvokers.Add(invoker);
        foreach (UnityAction<float> listener in freezerEffectListeners)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a freezer effect listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddFreezerEffectListener(UnityAction<float> listener)
    {
        // add listener to list and to all invokers
        freezerEffectListeners.Add(listener);
        foreach (PickupBlock invoker in freezerEffectInvokers)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    /// <summary>
    /// Adds the given script as a speedup effect invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddSpeedupEffectInvoker(PickupBlock invoker)
    {
        // add invoker to list and add all listeners to invoker
        speedupEffectInvokers.Add(invoker);
        foreach (UnityAction<float, float> listener in speedupEffectListeners)
        {
            invoker.AddSpeedupEffectListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a speedup effect listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        // add listener to list and to all invokers
        speedupEffectListeners.Add(listener);
        foreach (PickupBlock invoker in speedupEffectInvokers)
        {
            invoker.AddSpeedupEffectListener(listener);
        }
    }


    /// <summary>
    /// For PointsAddedEvent
    /// </summary>
    /// <param name="invoker"></param>
    public static void AddPointAddedEventInvoker(Block invoker)
    {
        // add invoker to list and add all listeners to invoker
        pointsAddedEventInvokers.Add(invoker);
        foreach (UnityAction<int> listener in pointsAddedEventListeners)
        {
            invoker.AddPointsAddedEffectListener(listener);
        }
    }

    public static void AddPointsAddedEffectListener(UnityAction<int> listener)
    {
        pointsAddedEventListeners.Add(listener);
        foreach (Block invoker in pointsAddedEventInvokers)
        {
            invoker.AddPointsAddedEffectListener(listener);
        }
    }

    /// <summary>
    ///  For BallsLeftEvent
    /// </summary>
    /// <param name="invoker"></param>
    public static void AddBallsLeftEffectInvoker(Ball invoker)
    {
        // add invoker to list and add all listeners to invoker
        ballsLeftEventInvokers.Add(invoker);
        foreach (UnityAction listener in ballsLeftEventListener)
        {
            invoker.AddBallsLeftEfectListener(listener);
        }
    }

    public static void AddBallsLeftEfectListener(UnityAction listener)
    {
        ballsLeftEventListener.Add(listener);
        foreach (Ball invoker in ballsLeftEventInvokers)
        {
            invoker.AddBallsLeftEfectListener(listener);
        }
    }

    public static void AddDeathBallEventInvoker(Ball invoker)
    {
        // add invoker to list and add all listeners to invoker
        deathBallEventInvokers.Add(invoker);
        foreach (UnityAction listener in deathBallEventListener)
        {
            invoker.AddDeathBallEventListener(listener);
        }
    }

    public static void AddDeathBallEventListener(UnityAction listener)
    {
        deathBallEventListener.Add(listener);
        foreach (Ball invoker in deathBallEventInvokers)
        {
            invoker.AddDeathBallEventListener(listener);
        }
    }

    public static void AddLastBallLostEventInvoker(HUD invoker)
    {
        lastBallLostEventInvokers.Add(invoker);
        foreach (UnityAction listener in lastBallLostEventListeners)
        {
            invoker.AddLastBallLostEventListener(listener);
        }
    }

    public static void AddLastBallLostEventListener(UnityAction listener)
    {
        lastBallLostEventListeners.Add(listener);
        foreach (HUD invoker in lastBallLostEventInvokers)
        {
            invoker.AddLastBallLostEventListener(listener);
        }
    }

    public static void AddLastBlockDestroyedEventInvoker(Block invoker)
    {
        lastBlockDestroyedEventInvokers.Add(invoker);
        foreach (UnityAction listener in lastBlockDestroyedEventListeners)
        {
            invoker.AddLastBlockDestroyedEventListener(listener);
        }
    }

    public static void AddLastBlockDestroyedEventListener(UnityAction listener)
    {
        lastBlockDestroyedEventListeners.Add(listener);
        foreach (Block invoker in lastBlockDestroyedEventInvokers)
        {
            invoker.AddLastBlockDestroyedEventListener(listener);
        }
    }

    #endregion
}
