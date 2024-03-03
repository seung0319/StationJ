using UnityEngine;
/// <summary>
/// POI 팝업 회전값 (항상 플레이를 바라보게) 계산 클래스
/// </summary>
public class PoiRotationSet : MonoBehaviour
{
    Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            //플레이어와 내 위치를 계산해 항상 플레이어를 바라보게 하는 코드
            Vector3 dir = this.transform.position - playerTransform.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 0.05f);
        }
    }

    //사용자 위치좌표 전달받는 함수
    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
