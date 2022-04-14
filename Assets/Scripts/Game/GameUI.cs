using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text pseudo;
    public Text score;
    public Text combo;
    public Text pourcent;
    public Image life;

    public Text subtitle;

    void Start()
    {
        pseudo.text = PlayerPrefs.GetString("username");
    }

    public void UpdateUI(int _score, int _combo, float _pourcent, float _life)
    {
        score.text = _score.ToString();
        combo.text = _combo.ToString();
        pourcent.text = _pourcent.ToString() + "%";
        life.rectTransform.sizeDelta = new Vector2(_life * 5, 0);
    }

    public void UpdateSubtitle(string Text)
    {
        subtitle.text = Text;
    }

}
