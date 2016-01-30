using UnityEngine;
using System.Collections;

public class RobotManager 
{
    public Color m_RobotColor { get; private set; }
    public float m_Health { get; private set; }

    public Color SetRobotColor( Color newColor )
    {
        m_RobotColor = newColor;

        return m_RobotColor;
    }
}
