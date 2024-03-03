using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��M�� ��ȸ�� ��ȸ�� �Ҷ� M ��� �ϴ� Ŭ����
/// </summary>
public class RemainingDistance : MonoBehaviour
{
    Transform player;
    [SerializeField]Text metersText;

    private void Update()
    {
        //�÷��̾� ��ġ�� �����޾� �Ÿ����
        if (player != null)
        {
            float distance = Vector3.Distance(player.position,transform.position);
            metersText.text = $"{(int)distance}M ��";
        }
    }

    //�÷��̾� ������ ���� �޾� �ʱ�ȭ �ϴ� �Լ�
    public void PlayerTransformSet(Transform player)
    {
        this.player = player;
    }
}
