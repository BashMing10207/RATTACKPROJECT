using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _trm;
    private void OnEnable()
    {
        GameManager.Instance.PlayerManagerCompo.GetCompo<CameraManager>().SetVCamTarget(_trm);
    }

    private void OnDisable()
    {
        //Invoke(nameof(ResetCam), 0.5f);
        ResetCam();
    }

    private void ResetCam()
    {
        GameManager.Instance.PlayerManagerCompo.GetCompo<CameraManager>().SetVCamTarget(GameManager.Instance.PlayerManagerCompo.GetCompo<AgentManager>(true).SelectedUnit().ViewPivot); //������ Ʈ��� �������� ���� �ſ� ����������....
    }
}
