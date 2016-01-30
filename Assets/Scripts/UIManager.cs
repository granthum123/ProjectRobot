using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    #region Variables
    ///Singleton
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

            return instance;
        }
    }

    private Canvas m_canvas;
    private Text m_timeText;
    private Text m_scoreText;
    private Slider m_healthSlider;

    private RobotManager m_robotManager;
    private GameManager m_gameManager;
    #endregion

    // Use this for initialization
    void Start () {
        m_gameManager = GameManager.Instance;
        m_robotManager = m_gameManager.m_RobotPrefab.GetComponent<RobotManager>();

        m_canvas = GetComponent<Canvas>();
        m_healthSlider = m_canvas.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        m_scoreText = m_canvas.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        m_timeText = m_canvas.transform.GetChild(1).GetChild(1).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        m_scoreText.text = "Score: " + m_gameManager.m_PlayerScore;
        m_timeText.text = "Time: " + m_gameManager.m_CurrentGameTime;
        m_healthSlider.value = m_robotManager.m_Health;
    }
}