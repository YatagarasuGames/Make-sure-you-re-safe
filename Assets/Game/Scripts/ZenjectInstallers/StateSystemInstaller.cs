using Zenject;

public class StateSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerState>().To<PlayerGameState>().AsSingle().NonLazy();
        Container.Bind<IPlayerState>().To<PlayerDialogueState>().AsSingle().NonLazy();

        Container.Bind<PlayerStateController>().FromNew().AsSingle();
    }
}
