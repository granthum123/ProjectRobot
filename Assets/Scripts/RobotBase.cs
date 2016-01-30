using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour
{
    public Color m_RobotColor { get; private set; }
    public float m_Health { get; private set; }

	void Start()
	{

	}

	void Update()
	{

	}

    public Color SetRobotColor( Color newColor )
    {
        m_RobotColor = newColor;

        return m_RobotColor;
    }
}
