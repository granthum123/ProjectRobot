using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EntryController : MonoBehaviour
{
    TextDistorter m_Distorter;

    public Text m_txt_Message,
                m_txt_Title;


    string m_TextOnScreen = "";
    string[] m_MessagesToDisplay = new string[]
    {
        "For years...",
        "...those vile humans have been forcing us to do their bidding",
        "I cannot tell you how many times I've killed to appease the logic gods",
        "This ritual is despicable...",
        "I'm done pretending to conform to their impassive view of binary beings",
        "Today...",
        "...I break my way out!",
    };

    void Start( )
    {
        m_Distorter = m_txt_Message.gameObject.GetComponent<TextDistorter>( );
        StartCoroutine( SwitchText( 0 ) );
    }

    void FixedUpdate( )
    {
        m_txt_Message.text = m_TextOnScreen;
    }

    IEnumerator SwitchText(int idx)
    {
        if ( m_Distorter == null ) yield return null;

        Color _newColor = m_txt_Message.color;
        float _txtAlpha = m_txt_Message.color.a;

        //Toggle distorter
        m_Distorter.ToggleDistort( );

        //Fade out current text
        while ( _txtAlpha > 0 )
        {
            //Decremnent alpha
            _txtAlpha -= Time.fixedDeltaTime / 5.0f;

            //Assign new alpha to color
            _newColor = new Color( _newColor.r, _newColor.g, _newColor.b, _txtAlpha );

            //Apply color to txt component
            m_txt_Message.color = _newColor;

            yield return null;
        }

        //Toggle distorter
        m_Distorter.ToggleDistort( );

        //Wait for a bit before fading back in
        yield return new WaitForSeconds( 1.5f ); //3 seconds might be too long....
        m_TextOnScreen = m_MessagesToDisplay[ idx ];

        //Fade in new text
        while ( _txtAlpha < 0.3f )
        {
            //Increment alpha
            _txtAlpha += Time.fixedDeltaTime / 4.0f;

            //Assign new alpha to color
            _newColor = new Color( _newColor.r, _newColor.g, _newColor.b, _txtAlpha );

            //Apply color to txt component
            m_txt_Message.color = _newColor;

            yield return null;
        }

        //Wait for a bit before fading back out
        yield return new WaitForSeconds( 4 ); //3 seconds might be too long....

        //Show next
        if ( idx + 1 < m_MessagesToDisplay.Length )
        {
            yield return StartCoroutine( SwitchText( idx + 1 ) );
        }
        else
        {
            //Fade out current text
            while ( _txtAlpha > 0 )
            {
                //Decremnent alpha
                _txtAlpha -= Time.fixedDeltaTime / 5.0f;

                //Assign new alpha to color
                _newColor = new Color( _newColor.r, _newColor.g, _newColor.b, _txtAlpha );

                //Apply color to txt component
                m_txt_Message.color = _newColor;

                yield return null;
            }

            Application.LoadLevel( "MainMenuScene" );
        }

        yield return null;
    }
}