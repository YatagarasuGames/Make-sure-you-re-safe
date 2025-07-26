using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerServicesInstaller : MonoInstaller
{
    [SerializeField] private FirstPersonMovement _movememnt;
    [SerializeField] private PlayerInteractService _interactService;
    [SerializeField] private FirstPersonLook _cameraController;
    [SerializeField] private DialogueUIBox _dialogueBox;
    public override void InstallBindings()
    {
        Container.Bind<FirstPersonMovement>().FromInstance(_movememnt).AsSingle();
        Container.Bind<PlayerInteractService>().FromInstance(_interactService).AsSingle();
        Container.Bind<FirstPersonLook>().FromInstance(_cameraController).AsSingle();
        Container.Bind<DialogueUIBox>().FromInstance(_dialogueBox).AsSingle();

        Container.Bind<PlayerControllersContainer>().FromNew().AsSingle();
    }
}
