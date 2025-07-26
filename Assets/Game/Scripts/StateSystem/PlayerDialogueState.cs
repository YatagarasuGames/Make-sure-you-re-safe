using UnityEngine;
using Zenject;

[PlayerStateType(PlayerStateType.Dialogue)]
public class PlayerDialogueState : IPlayerState
{
    public void EnterState(PlayerControllersContainer controllersContainer)
    {
        controllersContainer.CameraController.enabled = false;
        controllersContainer.InteractService.enabled = false;
        controllersContainer.Movement.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ExitState(PlayerControllersContainer controllersContainer) { }
}
