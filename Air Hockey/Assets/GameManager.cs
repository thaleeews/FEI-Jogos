using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Score {
        AiScore, PlayerScore
    }

    public Text AiScoreTxt, PlayerScoreTxt;
    private int AiScore, PlayerScore;

    public void Increment(Score whichScore) {
        if (whichScore == Score.AiScore) 
            AiScoreTxt.text = (++AiScore).ToString();
        else
            PlayerScoreTxt.text = (++PlayerScore).ToString();
    }

    /*
    public GUISkin layout;              // Fonte do placar
    GameObject theBall;                 // Referência ao objeto bola

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball"); // Busca a referência da bola

    }

    // Gerência da pontuação e fluxo do jogo
    void OnGUI () {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width - 160 - 12, 20, 100, 100), "PLAYER 1: " + PlayerScore1);
        GUI.Label(new Rect(Screen.width - 160 - 12, 45, 100, 100), "PLAYER 2: " + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width - 160 - 12, 75, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
        if (PlayerScore1 == 10)
        {
            GUI.Label(new Rect(Screen.width - 150, 200, 2000, 1000), "PLAYER ONE WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } else if (PlayerScore2 == 10)
        {
            GUI.Label(new Rect(Screen.width - 150, 200, 2000, 1000), "PLAYER TWO WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }*/
}
