using Cinemachine;
using UnityEngine;

namespace QuantumQuasars.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float camMinDist;
        [SerializeField] private float camMaxDist;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private CinemachineVirtualCamera virCam;

        private void Start()
        {
            SetCursor(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetCursor(true);

            if (Input.GetMouseButtonDown(0))
                SetCursor(false);

            Zoom();
        }

        private void Zoom()
        {
            float scroll = -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

            var comp = virCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            float curScroll = comp.m_CameraDistance;
            comp.m_CameraDistance = Mathf.Clamp(curScroll + scroll * Time.deltaTime, camMinDist, camMaxDist);
        }

        private void SetCursor(bool visible)
        {
            Cursor.visible = visible;
            Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
            var comp = virCam.GetCinemachineComponent<CinemachinePOV>();
            comp.enabled = !visible;
        }
    } 
}
