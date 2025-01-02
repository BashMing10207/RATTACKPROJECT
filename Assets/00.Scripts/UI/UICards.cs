using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICards : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IGetCompoable,IPointerClickHandler,IPointerDownHandler,IAfterInitable
{
    [SerializeField]
    private Outline _outLineRenderer;
    [SerializeField]
    private float offset = -60;

    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _actName;
    public ActSO Act { get; private set; }
    public bool IsSelected = false;
    public UnityEvent OnClicked;

    private RectTransform _rectTrm;
    private GetCompoParent _parent;
    private int _index = 0, _type =0;
    private PlayerAgentManager _manager;
    private ActCommander _commander;


    private void OnEnable()
    {
        _rectTrm = GetComponent<RectTransform>();
        OnClicked.AddListener(SetAction);
    }
    public void Init(ActSO act,int idx,int type)
    {
        Act = act;
        _actName.text = Act.ActName;
        _image.sprite = Act.Icon;
        _index = idx;
        _type = type;
    }

    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Lerp(transform.parent.forward, (transform.parent.forward + (Vector3)_parent.PlayerInput.MousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized, 0.1f),transform.parent.up),Time.deltaTime*10);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity;

    }

    public void AfterInit()
    {
        _manager = _parent.GetCompo<AgentManager>(true) as PlayerAgentManager;
        _commander = _parent.GetCompo<ActCommander>(true);
    }
    public void OutLineHandle(bool enabled)
    {
        IsSelected = enabled;
        _outLineRenderer.enabled = enabled;
        _rectTrm.localPosition = enabled ? new Vector3(0, 0, offset) : new Vector3(0, 0, -offset);
    }
    public void ToggleOutLine()
    {
        OutLineHandle(_outLineRenderer.enabled);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _parent.GetCompo<PlayerActions>().SetToolTip(eventData.pointerCurrentRaycast.worldPosition,Act.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _parent.GetCompo<PlayerActions>().DisableToolTip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void SetAction()
    {
        _commander.SetAct(Act);
        _parent.GetCompo<PlayerActions>().SetAction(_type,_index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _manager.IsHolding = false;
        OnClicked?.Invoke();
    }


}
