using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrog : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject beginDraggedItem;
    CanvasGroup group;
    public Vector3 startPosition;
    GameObject onDragParentGameObject;
    [SerializeField] Transform onDragParent;
    [HideInInspector] public Transform startParent;
    public Image delaysDrogImage;
    public float f_itemclikTime;
    private GraphicRaycaster gr;
    private PointerEventData ped;
    private List<RaycastResult> rrList;
    public ItemToolTip tip;
    public GameObject playerUI;
    public SkillTree skillTree;
    PutitemBag putItemBag;
    SkillItemFusion skillItemFusion;
    ItemType itemType;
    bool isDrag;
    public bool rayHit;
    public AudioClip onEndDragClip;
    void Start()
    {
        itemType = GetComponent<ItemType>();
        playerUI = GameObject.Find("Player UI");
        skillTree = playerUI.GetComponent<SkillTree>();
        onDragParentGameObject = GameObject.Find("Storage space");
        tip = onDragParentGameObject.GetComponent<ItemToolTip>();
        onDragParent = onDragParentGameObject.GetComponent<Transform>();
        group = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        putItemBag = FindAnyObjectByType<PutitemBag>();

        skillItemFusion = FindAnyObjectByType<SkillItemFusion>();
        gr = GetComponent<GraphicRaycaster>();
    }
    void InvokfillGauge()
    {
        tip.fillGauge = true;
    }
    private void Update()
    {

        //UI�� ���̸� ��� �ڵ�
        // PointerEventData�� �����ϰ� ���콺 ��ġ�� ����
        var ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;
        // Raycast ����� ������ ����Ʈ ����
        List<RaycastResult> results = new List<RaycastResult>();

        // GraphicRaycaster�� ���� Raycast ����
        gr.Raycast(ped, results);
        rayHit = false;
        // Raycast ��� Ȯ��
        foreach (var hitItem in results)
        {

            rayHit = true;
            for (int i = 0; i < hitItem.gameObject.transform.childCount; i++)
            {
                Debug.Log("ItemDropg.cs hitItem.name:" + hitItem.gameObject.name);
                if (hitItem.gameObject.name == "StackPotion" &&
                    Input.GetMouseButtonDown(1))
                {

                    skillTree.skillPoint += 5 * itemType.get_itemcount;
                    Destroy(hitItem.gameObject);
                    if (putItemBag.firstStackPostionCount < 1)
                    {
                        putItemBag.firstStackPostionCount += 1;
                    }
                    break;
                }
                if (delaysDrogImage != null)
                {
                    break; // ù ��° Image ������Ʈ�� ã���� ����
                }
            }


            // ���콺 Ŭ�� ���� Ȯ�� && ���̰� �� ���� ���� ������Ʈ�� layer�� �������� ���
            if (Input.GetMouseButton(0))
            {
                // Ŭ�� �ð� ����
                f_itemclikTime += Time.deltaTime;
                delaysDrogImage.fillAmount = f_itemclikTime;
            }
            else
            {
                // �ʱ�ȭ
                if (delaysDrogImage != null)
                {
                    f_itemclikTime = 0;
                    delaysDrogImage.fillAmount = 0;
                }
            }
        }
        if (delaysDrogImage != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                f_itemclikTime = 0;
                delaysDrogImage.fillAmount = 0;
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� fillAmount ���� Ȯ��
        if (delaysDrogImage == null || delaysDrogImage.fillAmount < 1)
        {
            isDrag = false; // �巡�� ���¸� false�� ����
            return;
        }

        isDrag = true;
        beginDraggedItem = gameObject;

        // �巡�� ���� �� ���� ��ġ�� �θ� ����
        startPosition = transform.position;
        startParent = transform.parent;

        // �巡�� �� blocksRaycasts ��Ȱ��ȭ
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;

        // �巡�� ���¿��� �θ� onDragParent�� ����
        transform.SetParent(onDragParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ���°� true�� ���� �۵�
        if (!isDrag)
            return;

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;
        SoundManager.PlaySound(onEndDragClip);
        // �巡�� ���� �Ǵ� ���� �ʱ�ȭ
        if (!isDrag)
        {
            RestoreOriginalState(); // ���� ��ġ�� ����
            return;
        }

        // �巡�� ���� �� ���� �ʱ�ȭ
        isDrag = false; // �巡�� ���� ����
        f_itemclikTime = 0;

        if (delaysDrogImage != null)
            delaysDrogImage.fillAmount = 0;

        skillItemFusion.corutinitem = true; // �߰� ���� ����
    }

    // ���� �θ�� ��ġ�� ����
    private void RestoreOriginalState()
    {
        // ���� ��ġ�� �θ� ��ȿ���� Ȯ��
        if (startParent != null)
        {
            transform.SetParent(startParent); // ���� �θ�� ����
            transform.position = startPosition; // ���� ��ġ�� ����
        }
        else
        {
            Debug.LogWarning("Start parent is null. Cannot restore item.");
        }

        // ���� �ʱ�ȭ
        isDrag = false;
        f_itemclikTime = 0;

        if (delaysDrogImage != null)
            delaysDrogImage.fillAmount = 0;
    }
}
