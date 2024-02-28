using UnityEngine;
/// <summary>
/// POI �˾� ȸ���� (�׻� �÷��̸� �ٶ󺸰�) ��� Ŭ����
/// </summary>
public class PoiRotationSet : MonoBehaviour
{
    Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            //�÷��̾�� �� ��ġ�� ����� �׻� �÷��̾ �ٶ󺸰� �ϴ� �ڵ�
            Vector3 dir = this.transform.position - playerTransform.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 0.05f);
        }
    }

    //����� ��ġ��ǥ ���޹޴� �Լ�
    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
