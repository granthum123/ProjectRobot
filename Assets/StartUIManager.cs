using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartUIManager : MonoBehaviour {

    private Canvas m_canvas;
    public Text m_Weapon1Text;
    public Text m_Weapon2Text;
    private string[] m_WeaponNames = new string[] { "Lazer Pistol", "Minigun", "Rocket Launcher", "Shotgun", "Assault Rifle" };
    private int m_Weapon1num = 0;
    private int m_Weapon2num = 0;
    // Use this for initialization
    void Start () {
        m_Weapon1Text.text = m_WeaponNames[m_Weapon1num];
        m_Weapon2Text.text = m_WeaponNames[m_Weapon2num];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        //start game
    }

    public void Weapon1Next()
    {
        m_Weapon1num++;
        if (m_Weapon1num == m_WeaponNames.Length)
            m_Weapon1num = 0; 
        m_Weapon1Text.text = m_WeaponNames[m_Weapon1num];
    }
    public void Weapon1Prev()
    {
        m_Weapon1num--;
        if (m_Weapon1num == -1)
            m_Weapon1num = m_WeaponNames.Length-1;
        m_Weapon1Text.text = m_WeaponNames[m_Weapon1num];
    }
    public void Weapon2Next()
    {
        m_Weapon2num++;
        if (m_Weapon2num == m_WeaponNames.Length)
            m_Weapon2num = 0;
        m_Weapon2Text.text = m_WeaponNames[m_Weapon2num];
    }
    public void Weapon2Prev()
    {
        m_Weapon2num--;
        if (m_Weapon2num == -1)
            m_Weapon2num = m_WeaponNames.Length - 1;
        m_Weapon2Text.text = m_WeaponNames[m_Weapon2num];
    }
}
