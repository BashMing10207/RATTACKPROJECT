 
using UnityEngine;

public class EnemyManager : AgentManager
{

    //�� ������Ʈ:
    //1.������ġ(�ڿ��ִ� ���� ��������) (���� �÷��̾� ���̿� ��ֹ��� �ְų� ���� �� ���� �̵�)
    //2.���Ÿ� ���� (�÷��̾�� �� ���̿� ��ֹ��� ���� ��).
    //3.�������� (�÷��̾�� ���� ������ ��Ÿ� ���� �ִ� ��� ���
    //4.����Ʈ (���ڸ� ���� ��) (�������ݰ� ����)
    //5.����(���̹ޱ�) �� ����͵� �ش���� ������, �����̱⸸ ���� ��.

    protected override void Start()
    {
        base.Start();
        //MultiGameManager.Instance.PlayerManagerCompo.Add(this);
    }
}
