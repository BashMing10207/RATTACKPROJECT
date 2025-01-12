using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IGetCompoable, IAfterInitable
{
    private GetCompoParent _parent;

    private ItemManager _itemManager;

    private AgentManager _enemyAgentManager;

    private AgentManager _playerAgentManager;
    private ActCommander _commander;

    private CameraManager _cameraManager;

    private List<ActSO> _recycleItems = new();

    [SerializeField]
    private LayerMask _whatIsObjstacle;

    [SerializeField]
    private float _waitTime = 1.5f;

    public Unit SelectedUnit { get; private set; }

    public Vector3 ActDir { get; private set; }

    public ActSO CurrentAct { get; private set; }

    private List<Unit> _enemyUnits => _enemyAgentManager.Units;
    private List<Unit> _playerUnits => _playerAgentManager.Units;

    public int MAXCount = 15;
    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;
    }
    public void AfterInit()
    {
        _itemManager = _parent.GetCompo<ItemManager>();
        _enemyAgentManager = _parent.GetCompo<AgentManager>(true);
        _commander = _parent.GetCompo<ActCommander>(true);
    }

    private void Start()
    {
        _playerAgentManager = GameManager.Instance.PlayerManagerCompos[0].GetCompo<AgentManager>(true);
        _cameraManager = _parent.GetCompo<CameraManager>();

        GameManager.Instance.OnTurnEndEvent += EnemyTurn;
        //EnemyTurn();
    }

    public void EnemyTurn()
    {
        if (!GameManager.Instance.IsPlayerturn)
        {
            _parent.gameObject.SetActive(true);
            if (_enemyAgentManager.Units.Count <= 0)
            {
                GameManager.Instance.OnTurnEnd();
                return;
            }
            StartCoroutine(EnemyPlay());
        }
        else
        {
            _parent.gameObject.SetActive(false);
            print("�ֳʹ� ���� ����");
        }
    }

    protected IEnumerator EnemyPlay()
    {
        _itemManager.Items.AddRange(_recycleItems);// ���� �Ͽ� ������� ����(����) �����۵�
        _recycleItems.Clear();
        int count = 0;

        while (_itemManager.Items.Count > 0&& count < MAXCount )
        {
            count++;
            yield return new WaitForSeconds(_waitTime);
            if (_enemyAgentManager.Units.Count == 0)
            {
                break;
            }

            bool canUse = DoAction();

            yield return new WaitForSeconds(0.3f);
            if (canUse) 
            {
                _cameraManager.SetVCamTarget(SelectedUnit.ViewPivot);
                yield return new WaitForSeconds(0.4f);
                RunAction();
            }
        }

        GameManager.Instance.OnTurnEnd();
    }

    public void RunAction()
    {
        _commander.SetAct(CurrentAct);
        _enemyAgentManager.SelectedUnitIdx = _enemyAgentManager.Units.IndexOf(SelectedUnit);
        _commander.TrySkill(ActDir, CurrentAct);
        //CurrentAct.RunAct(ActDir, SelectedUnit);
    }

    public bool DoAction()
    {
        int selectIndex = Random.Range(0, _itemManager.Items.Count);
        bool canUse = true;

        switch (_itemManager.Items[selectIndex].ActTypeEnum)
        {
            case ActType.Projectile:
                canUse = ActProjectile(_itemManager.Items[selectIndex]);
                break;
            case ActType.Impact:
                canUse = ActImpact(_itemManager.Items[selectIndex]);
                break;
            case ActType.Melee:
                canUse = ActMelee(_itemManager.Items[selectIndex]);
                break;
            case ActType.Move:
                canUse = ActMove(_itemManager.Items[selectIndex]);
                break;
            case ActType.Buff:
                canUse = ActBuff(_itemManager.Items[selectIndex]);
                break;
                //case ActType.Passive:

                //    break;
        }
        _commander.SetAct(CurrentAct);

        print(canUse);

        if (!canUse)
            _recycleItems.Add(_itemManager.Items[selectIndex]);

        _itemManager.Items.RemoveAt(selectIndex);

        return canUse;
    }

    //�� �Ʒ��� � ������ ��ŭ�� ���� �������� �ൿ������ �����Ͽ� ActSO�� RunAct()�� ȣ���Ѵ�.

    private bool ActProjectile(ActSO so)
    {
        CurrentAct = so;
        float distance = 10;

        List<SetablePair<Unit, Unit>> attackablePairs = new();

        for (int i = 0; i < _enemyAgentManager.Units.Count; i++)
        {
            for (int j = 0; j < _playerAgentManager.Units.Count; j++)
            {
                if (Vector3.Distance(_enemyUnits[i].transform.position, _playerUnits[j].transform.position) < distance)
                {
                    if (!IsObstacle(_enemyUnits[i].transform.position, _playerUnits[j].transform.position))
                    {
                        attackablePairs.Add(new(_enemyUnits[i], _playerUnits[j]));
                    }
                }
            }

        }

        if (attackablePairs.Count > 0)
        {
            int rand = Random.Range(0, attackablePairs.Count);
            SelectedUnit = attackablePairs[rand].First;
            _enemyAgentManager.SelectedUnitIdx = rand;
            ActDir = (attackablePairs[rand].Second.transform.position - attackablePairs[rand].First.transform.position);

            ActDir = ActDir.normalized * ActDir.magnitude/1.2f;
            //so.RunAct((attackablePairs[rand].First.transform.position - attackablePairs[rand].First.transform.position), attackablePairs[rand].First);
            return true;
        }

        return false;
    }
    private bool ActImpact(ActSO so)
    {

        return ActProjectile(so);
    }

    private bool ActMelee(ActSO so)
    {

        return ActProjectile(so);
    }

    private bool ActMove(ActSO so)
    {

        CurrentAct = so;
        float distance = 10;

        List<SetablePair<Unit, Unit>> attackablePairs = new();

        for (int i = 0; i < _enemyAgentManager.Units.Count; i++)
        {
            for (int j = 0; j < _playerAgentManager.Units.Count; j++)
            {
                if (Vector3.Distance(_enemyUnits[i].transform.position, _playerUnits[j].transform.position) < distance)
                {
                    if (!IsObstacle(_enemyUnits[i].transform.position, _playerUnits[j].transform.position))
                    {
                        attackablePairs.Add(new(_enemyUnits[i], _playerUnits[j]));
                    }
                }
            }

        }

        if (attackablePairs.Count > 0)
        {
            int randEnemy = Random.Range(0, attackablePairs.Count);
            SelectedUnit = attackablePairs[randEnemy].First;
            ActDir = (attackablePairs[randEnemy].Second.transform.position - attackablePairs[randEnemy].First.transform.position);

            _cameraManager.SetVCamTarget(SelectedUnit.ViewPivot);
            //so.RunAct((attackablePairs[rand].First.transform.position - attackablePairs[rand].First.transform.position), attackablePairs[rand].First);
            return true;
        }

        int rand = Random.Range(0, _enemyUnits.Count);
        int randPlayer = Random.Range(0, _playerUnits.Count);
        SelectedUnit = _enemyUnits[rand];
        ActDir = (_playerUnits[randPlayer].transform.position - _enemyUnits[rand].transform.position);

        _cameraManager.SetVCamTarget(SelectedUnit.ViewPivot);

        return true;
    }

    private bool ActBuff(ActSO so)
    {
        int rand = Random.Range(0, _enemyUnits.Count);
        so.RunAct(new Vector3(Random.Range(-10, 10), 0, 0), _enemyUnits[rand]);
        return true;
    }


    private bool IsObstacle(Vector3 origin, Vector3 pos)
    {
        Vector3 dir = pos - origin;

        return Physics.Raycast(origin, dir, dir.magnitude, _whatIsObjstacle);

    }

}
