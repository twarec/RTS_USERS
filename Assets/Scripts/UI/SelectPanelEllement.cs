using UnityEngine.UI;
using UnityEngine;

public class SelectPanelEllement : MonoBehaviour
{
    public string Name;
    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            CountText.text = _count.ToString();
        }
    }


    private Image image;
    [SerializeField]
    private Text CountText;


    private int _count;

    private void Awake()
    {
        image = GetComponent<Image>();
        
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public bool AddUnit(RTS.ISelectebleObject selecteble)
    {
        if (Count < 1)
        {
            gameObject.SetActive(true);
            image.sprite = selecteble.Icon;
            Name = selecteble.Name;
        }else
        if (selecteble.Name != Name)
        {
            return false;
        }
   
        Count++;
        return true;
    }
    public bool RemoveUnit(RTS.ISelectebleObject selecteble)
    {
        if(selecteble.Name != Name)
        {
            return false;
        }

        if(Count == 1)
        {
            gameObject.SetActive(false);
            image.sprite = null;
            Name = null;
        }
        Count--;
        return true;
    }
}
