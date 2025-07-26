using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractableObjectsInstaller : MonoInstaller
{
    [SerializeField] private Bed _bed;
    public override void InstallBindings()
    {
        Container.Bind<Bed>().FromInstance(_bed).AsSingle().NonLazy();
    }
}
