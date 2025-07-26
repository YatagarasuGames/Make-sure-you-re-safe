using System.Collections.Generic;
using Zenject;
using System.Linq;
using System.Reflection;

public class PlayerStateController
{
    [Inject] private PlayerControllersContainer _controllers;
    private IPlayerState _currentState;
    [Inject] private List<IPlayerState> _states;

    public void ChangeState(PlayerStateType newPlayerState)
    {
        _currentState?.ExitState(_controllers);
        _currentState = _states.FirstOrDefault(s => 
            (PlayerStateType)s.GetType().GetCustomAttribute<PlayerStateTypeAttribute>()?.StateType == newPlayerState);
        
        _currentState?.EnterState(_controllers);
    }
}
