using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWithActivationTrigger : Window
{
    private bool _isTriggered = false;
    [SerializeField] private GameObject _objectToActivate;

    protected override void Close()
    {
        base.Close();
        if (!_isTriggered)
        {
            _isTriggered = true;
            _objectToActivate.SetActive(true);
        }
    }

}
