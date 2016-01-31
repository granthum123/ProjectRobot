using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void BeginGame( )
    {
        Application.LoadLevel( "BattleScene" );
    }
}