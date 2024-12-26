using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICards : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IGetCompoable,IPointerClickHandler,IPointerDownHandler
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
    private PlayerManager _parent;
    private int _index = 0, _type =0;

    private void Awake()
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

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Initialize(GetCompoParent entity)
    {
        _parent = entity as PlayerManager;
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
        _parent.SetAct(Act);
        _parent.GetCompo<PlayerActions>().SetAction(_type,_index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _parent.IsHolding = false;
        OnClicked?.Invoke();
    }
}
