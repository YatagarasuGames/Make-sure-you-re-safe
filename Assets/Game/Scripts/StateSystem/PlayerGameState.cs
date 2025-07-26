using UnityEngine;

[PlayerStateType(PlayerStateType.Game)]
public class PlayerGameState : IPlayerState
{
    public void EnterState(PlayerControllersContainer controllersContainer)
    {
        controllersContainer.CameraController.enabled = true;
        controllersContainer.InteractService.enabled = true;
        controllersContainer.Movement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitState(PlayerControllersContainer controllersContainer) { }
}
