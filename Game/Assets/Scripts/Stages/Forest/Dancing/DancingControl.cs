using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SwipeDetector))]
public class DancingControl : MonoBehaviour {

    public GameObject ArrowPrefab;
    public GameObject HitZone;
    public GameObject MiniGamePanel;
    public GameObject GreenPanel;

    public UnityEvent CorrectButton = new UnityEvent();
    public UnityEvent Fail = new UnityEvent();
    public int PreLoadedPrefabs;

    public float time;
    private float timeDuration;
    private bool started = false;
    private bool m_isNeedToPushKey = false;
    private ArrowFactory factory;
    private CurrentDirection currentDirection;
    private SwipeDetector m_swipeDetector;

    // Use this for initialization
    private void Awake()
    {
        m_swipeDetector = GetComponent<SwipeDetector>();

        factory = new ArrowFactory(ArrowPrefab, PreLoadedPrefabs, MiniGamePanel);
        currentDirection = GetComponent<CurrentDirection>();
    }

    void Start () {
     
        CorrectButton.AddListener(() => { Debug.Log("Right"); });
        CorrectButton.AddListener(GreenLighter);
        Fail.AddListener(() => { Debug.Log("Fail"); });
        
	}

    private void OnEnable()
    {
        m_swipeDetector.OnSwipe.AddListener(OnSwipe);
    }

    private void OnDisable()
    {
        m_swipeDetector.OnSwipe.RemoveListener(OnSwipe);
    }

    // Update is called once per frame
    void Update () {
		if(started)
        {
            if (timeDuration <= 0)
            {
                var copy = factory.getArrow();
                timeDuration = time;
            }
            else
                timeDuration -= Time.deltaTime;

            if(Input.anyKeyDown)
            {
                checkKey();
            }
        }
	}

    public void ArrowCome()
    {
        m_isNeedToPushKey = true;
    }

    public void ArrowLeave()
    {
        if(m_isNeedToPushKey && started)
            Fail.Invoke();

    }

    public void startGame()
    {
        started = true;
    }

    public void finishGame()
    {
        started = false;
        factory.Refil();
    }

    public void OnSwipe(SwipeDirection swipeDirection)
    {
        var arrowDirection = currentDirection.CurrentArrowDirection;
        if (m_isNeedToPushKey)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.UP:
                    ApplyInput(arrowDirection == GameArrowDirection.UP);
                    break;
                case SwipeDirection.DOWN:
                    ApplyInput(arrowDirection == GameArrowDirection.DOWN);
                    break;
                case SwipeDirection.LEFT:
                    ApplyInput(arrowDirection == GameArrowDirection.LEFT);
                    break;
                case SwipeDirection.RIGHT:
                    ApplyInput(arrowDirection == GameArrowDirection.RIGHT);
                    break;
            }
        }
    }

    private void checkKey()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!m_isNeedToPushKey)
                return;
            else
            if (HitZone.GetComponent<CurrentDirection>().CurrentArrowDirection == GameArrowDirection.UP)
            {
                CorrectButton.Invoke();
                m_isNeedToPushKey = false;
            }
            else
                Fail.Invoke();
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!m_isNeedToPushKey)
                return;
            else
               if (HitZone.GetComponent<CurrentDirection>().CurrentArrowDirection == GameArrowDirection.DOWN)
            {
                CorrectButton.Invoke();
                m_isNeedToPushKey = false;
            }
            else
                Fail.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!m_isNeedToPushKey)
                return;
            else
            if (HitZone.GetComponent<CurrentDirection>().CurrentArrowDirection == GameArrowDirection.LEFT)
            {
                CorrectButton.Invoke();
                m_isNeedToPushKey = false;
            }
            else
                Fail.Invoke();
        }
        else
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!m_isNeedToPushKey)
                return;
            else
            if (HitZone.GetComponent<CurrentDirection>().CurrentArrowDirection == GameArrowDirection.RIGHT)
            {
                CorrectButton.Invoke();
                m_isNeedToPushKey = false;
            }
            else
                Fail.Invoke();
        }
    }

    private void ApplyInput(bool result)
    {
        if (result)
        {
            CorrectButton.Invoke();
            m_isNeedToPushKey = false;
        }
        else
        {
            Fail.Invoke();
        }
    }

    private void GreenLighter()
    {
        GreenPanel.SetActive(true);
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(OffGreen());
        }
        else
        {
            GreenPanel.SetActive(false);
        }
    }
    private IEnumerator OffGreen()
    {
        yield return new WaitForSeconds(0.8f);
        GreenPanel.SetActive(false);
    }
}

public class ArrowFactory
{
    List<GameObject> arrows;
    GameObject ArrowPrefab;
    GameObject MiniGamePanel;
    int capacity; 
    int XRange;
    public ArrowFactory(GameObject arrowPrefab, int capacity, GameObject MiniGamePanel)
    {
      
        this.MiniGamePanel = MiniGamePanel;
        ArrowPrefab = arrowPrefab;
        this.capacity = capacity;
        XRange = (int)(MiniGamePanel.GetComponent<RectTransform>().rect.width / 2 + arrowPrefab.GetComponent<RectTransform>().rect.width / 2);
        Refil();
    }

    private int index = 0;
    public GameObject getArrow()
    {
        int Pos = index;

        do
        {
            if (Pos >= arrows.Count)
                Pos = 0;
            if (arrows[Pos].GetComponent<ArrowDrive>().Free)
            {
                arrows[Pos].GetComponent<ArrowDrive>().Move();
                arrows[Pos].GetComponent<ArrowDrive>().ArrowDirection = (GameArrowDirection)Random.Range(1, 5);
                arrows[Pos].transform.localPosition = new Vector3(XRange, 0, 0);
                ++index;
                if (index >= arrows.Count)
                    index = 0;
                return arrows[Pos];
            }
            ++Pos;
        } while (Pos != index);

        var copy = GameObject.Instantiate(ArrowPrefab);
        copy.transform.SetParent(MiniGamePanel.transform, false);
        arrows.Add(copy);
        return getArrow();
    }
    public void Refil()
    {
        if(arrows != null)
        for(int i = 0; i < arrows.Count; i++)
        {
            GameObject.Destroy(arrows[i].gameObject);
        }

        arrows = new List<GameObject>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            var copy = GameObject.Instantiate(ArrowPrefab);
            copy.transform.SetParent(MiniGamePanel.transform, false);
            copy.transform.localPosition = new Vector3(XRange, 0, 0);
            arrows.Add(copy);
        }
    }
}

public enum GameArrowDirection
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}