using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Events;
using System;

public class GameManager : Singleton<GameManager>
{
    public GlobalGameState gameState;

    public static BaseGameEvent<GlobalGameState> OnGameStateChanged;

    private void Awake()
    {
        gameState = GlobalGameState.GlobalContinue;
    }
    public void UpdateGameState(GlobalGameState newState)
    {
        switch (newState)
        {
            case GlobalGameState.GlobalPause:
                break;
            case GlobalGameState.GlobalContinue:
                break;
            case GlobalGameState.GlobalMainMenu:
                break;
            case GlobalGameState.PlayerMenu:
                HandlePlayerMenu();
                break;
            case GlobalGameState.PlayerControl:
                HandlePlayerControl();
                break;
            case GlobalGameState.PlayerInteraction:
                break;
            case GlobalGameState.Passage:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Raise(newState);
    }

    private void Start()
    {
        UpdateGameState(GlobalGameState.PlayerControl);
    }

    private void HandlePlayerControl()
    {
        throw new NotImplementedException();
    }

    private void HandlePlayerMenu()
    {
        throw new NotImplementedException();
    }
}