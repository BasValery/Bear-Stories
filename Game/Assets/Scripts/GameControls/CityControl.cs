using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityControl : MonoBehaviour {
    #region Fields
    public static CityControl instance = null;

    public float m_xFrom = 0f;
    public float m_xTo = 50f;
    public Light m_sunLight;
    public GameObject m_mainHero;
    public GameObject m_characters;
    public GameObject m_creatures;
    public GameObject m_npcPool;

    
    private List<CloudController> m_clouds = null;
    private CameraController m_cameraController;
    private GameTime m_gameTime;
    private string m_initialSceneName;
    #endregion //Fields

    #region Messages
    // Use this for initialization
    private void Start () {
        Global.Hud.hudEnable();
        m_cameraController = Camera.main.GetComponent<CameraController>();
        m_cameraController.SetLimits(m_xFrom, m_xTo);

        Global.Time.Freeze(true);
        var guideDialogue = new GuideDialogue();
        var arrowController = Global.Hud.m_arrowcontroller;
        guideDialogue.AddSentence("Как быстро идёт время!");
        guideDialogue.AddSentence("Тебе пора брать бизнес в свои руки!");
        guideDialogue.AddSentence("Давай быстро всё повторим");
        guideDialogue.AddSentence("Это твой капитал, с него ты платишь аренду. Выполняй заказы качественно, а то останешься на улице", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_money.GetComponent<RectTransform>(),
                ArrowDirection.DOWNLEFT
            );
        });
        guideDialogue.AddSentence("Это твой респект, он влияет на количество твоих клиентов", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_respect.GetComponent<RectTransform>(),
                ArrowDirection.DOWNLEFT
                );
        });

        guideDialogue.AddSentence("Здесь отображаются все твои заказы, и сколько времени осталось на их выполнение", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_tasks.GetComponent<RectTransform>(),
                ArrowDirection.DOWN
                );
        });

        guideDialogue.AddSentence("Не забывай следить за временем, с наступлением ночи лавка закроется", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_clock.GetComponent<RectTransform>(),
                ArrowDirection.RIGHTDOWN
                );
        });

        arrowController.Hide();
        guideDialogue.AddSentence("Задачи на день ты всегда найдёшь здесь", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_showTasksButton.GetComponent<RectTransform>(),
                ArrowDirection.DOWNLEFT
                );
            arrowController.PointOut(
                Global.Hud.m_showTasksButton.GetComponent<RectTransform>(),
                ArrowDirection.DOWN
                );
        });

        guideDialogue.AddSentence("Тут ты можешь настроить и покинуть игру", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_settingsButton.GetComponent<RectTransform>(),
                ArrowDirection.DOWN
                );
        });

        guideDialogue.AddSentence("Инвентарь всегда под рукой", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_inventoryButton.GetComponent<RectTransform>(),
                ArrowDirection.DOWN
                );
        });

        guideDialogue.AddSentence("Не забывай заглядывать и смотреть информацию об ингредиентах, зельях, персонажах!", () =>
        {
            arrowController.PointOut(
                Global.Hud.m_marksPanel.GetComponent<RectTransform>(),
                ArrowDirection.LEFT
                );
        });

        guideDialogue.AddSentence("И постарайся найти для меня резиновых уток, их можно встретить где угодно, я отблагодарю тебя за это");

        guideDialogue.AddSentence("Теперь ты почти готов", () =>
        {
            arrowController.Hide();
            Global.Time.Freeze(false);
        });

        guideDialogue.AddSentence("Рабочий день начался, но у тебя пока нет клиентов, можешь сходить к гному, разузнать как у него дела!");

        Global.Hud.GuideManager.StartDialouge(guideDialogue);
	}


    private void Awake()
    {
        #region Singleton
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            m_initialSceneName = gameObject.scene.name;
        }


        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            return;
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        #endregion //Singleton
       
        m_clouds = GetComponentsInChildren<CloudController>().ToList();
        m_gameTime = Global.Time;

        foreach (CloudController cloud in m_clouds)
        {
            cloud.m_xMin = m_xFrom - 4;
            cloud.m_xMax = m_xTo + 4;
        }

        if (m_gameTime.IsFreezed == false)
        {
            m_clouds.ForEach(x => x.Launch());
        }

        m_gameTime.TimeFreezed += OnGameTimeFreezed;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }


    private void OnDestroy()
    {
        if (this == instance)
        {
            if (m_gameTime != null)
            {
                m_gameTime.TimeFreezed -= OnGameTimeFreezed;
            }
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            if (m_clouds != null)
            {
                m_clouds.ForEach(x => x.Stop());
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Global.Time.Freeze(!Global.Time.IsFreezed);
        }
    }
    #endregion //Messages

    #region Methods
    private void OnGameTimeFreezed(object sender, TimeFreezedEventArgs e)
    {
        if (m_clouds != null)
        {
            m_clouds.ForEach(x => {
                if (e.Freezed)
                    x.Stop();
                else
                    x.Launch();
            });
        }
    }

    private void OnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        var childRenderers = GetComponentsInChildren<Renderer>();

        bool enabled = (m_initialSceneName == arg1.name);

        if (enabled)
        {
            EnableCharacters(m_characters, true);
            EnableCharacters(m_creatures, true);
            EnableNPCes(m_npcPool, true);
            m_cameraController = Camera.main.GetComponent<CameraController>();
            if (m_cameraController)
            {
                m_cameraController.SetLimits(m_xFrom, m_xTo);
            }

        }
        else
        {
            EnableCharacters(m_characters, false);
            EnableCharacters(m_creatures, false);
            EnableNPCes(m_npcPool, false);
        }

        if (m_sunLight != null)
        {
            m_sunLight.enabled = enabled || arg1.name == "Forest";
        }
        else
        {
            Debug.LogWarning("GlobalSunSource is not found");
        }

        if (enabled)
        {
            m_clouds.ForEach(x => x.Launch());
        }
        else
        {
            m_clouds.ForEach(x => x.Stop());
        }

        Global.Hud.GuideManager.StopDialogue();
        Global.Hud.m_arrowcontroller.Hide();
        Global.Time.Freeze(false);

    }

    private void EnableNPCes(GameObject gameObject, bool enable)
    {
        Buyer npc;
        foreach (Transform childTransform in gameObject.transform)
        {
            var child = childTransform.gameObject;
            foreach (Transform childChildTransform in child.transform)
            {
                npc = childChildTransform.gameObject.GetComponent<Buyer>();
                if (npc != null)
                {
                    EnableCharacter(npc.gameObject, enable);
                }
                else
                {
                    SetChildrenChildrenActive(childChildTransform.gameObject, enable);
                }
            }
        }
    }

    private void EnableCharacters(GameObject gameObject, bool enable)
    {

        foreach (Transform childTransform in gameObject.transform)
        {
            var childObject = childTransform.gameObject;
            EnableCharacter(childObject, enable);

        }
    }

    private void EnableCharacter(GameObject gameObject, bool enable)
    {

        SetChildrenChildrenActive(gameObject, enable);

        var animator = gameObject.GetComponent<Animator>();
        var collider = gameObject.GetComponent<Collider2D>();
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var audio = gameObject.GetComponent<AudioSource>();
        if(audio)
        {
            audio.enabled = enable;
        }
        if (animator)
        {
            animator.enabled = enable;
        }

        if (collider)
        {
            collider.enabled = enable;
        }

        if (rigidBody)
        {
            rigidBody.isKinematic = enabled;
        }
    }

    private void SetChildrenChildrenActive(GameObject gameObject, bool b)
    {
        foreach (Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.SetActive(b);
        }
    }

    #endregion //Methods
}
