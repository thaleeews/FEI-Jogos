using UnityEngine;
using Unity.Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;

    private void SetFollow(GameObject player)
    {
        virtualCamera.Follow = player.transform;
    }
    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetFollow;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetFollow;
    }
}
