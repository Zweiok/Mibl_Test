using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followPlayerCamera;
    [SerializeField] private CinemachineVirtualCamera uiCamera;

    public void SetupFollowData(Transform lookAt, Transform follow)
    {
        followPlayerCamera.LookAt = lookAt;
        followPlayerCamera.Follow = follow;
    }

    public void SetCamera(CameraType type)
    {
        followPlayerCamera.gameObject.SetActive(type == CameraType.FollowPlayerCamera);
        uiCamera.gameObject.SetActive(type == CameraType.UICamera);
    }
}
