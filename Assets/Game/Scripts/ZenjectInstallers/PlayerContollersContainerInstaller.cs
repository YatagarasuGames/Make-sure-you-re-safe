using UnityEngine;
using Zenject;
using UnityEngine;

public class PlayerContollersContainerInstaller : Installer<PlayerContollersContainerInstaller>
{
    [SerializeField] private FirstPersonLook _playerCamera;
    [SerializeField] private FirstPersonMovement _movement;
    [SerializeField] private PlayerInteractService _interactService;
    public override void InstallBindings()
    {
        Container.Bind<PlayerControllersContainer>().FromNew().AsSingle().WithArguments(
            _movement,
            _playerCamera,
            _interactService
        );
    }

}