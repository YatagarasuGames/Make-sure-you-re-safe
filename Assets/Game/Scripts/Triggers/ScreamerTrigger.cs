using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ScreamerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _screamerUI;
    [Inject] private PlayerStateController _stateController;

    [SerializeField] private GameObject _screamerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _stateController.ChangeState(PlayerStateType.Dialogue);
        //CreateScreamer2D(other);
        CreateScreamer3D(other);

    }

    private void CreateScreamer2D(Collider other)
    {
        Instantiate(_screamerUI, other.GetComponentInChildren<Canvas>().gameObject.transform);
        gameObject.SetActive(false);
    }

    private void CreateScreamer3D(Collider other)
    {
        other.gameObject.SetActive(false);
        _screamerCamera.SetActive(true);
        Instantiate(_screamerUI, _screamerCamera.GetComponentInChildren<Canvas>().gameObject.transform);
        if (GetComponent<Animator>() != null) GetComponent<Animator>()?.SetBool("IsChasing", false);
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        }
    }
}
