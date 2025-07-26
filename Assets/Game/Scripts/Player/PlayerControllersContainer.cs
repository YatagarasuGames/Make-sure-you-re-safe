using UnityEngine;

public class PlayerControllersContainer
{
    public FirstPersonMovement Movement { get; }
    public FirstPersonLook CameraController { get; }
    public PlayerInteractService InteractService { get; }

    public PlayerControllersContainer(
        FirstPersonMovement movement,
        FirstPersonLook cameraController,
        PlayerInteractService interactService)
    {
        Movement = movement;
        CameraController = cameraController;
        InteractService = interactService;
    }
}
