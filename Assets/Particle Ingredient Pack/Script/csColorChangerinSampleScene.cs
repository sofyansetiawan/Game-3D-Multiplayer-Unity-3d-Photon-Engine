using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class csColorChangerinSampleScene : MonoBehaviour {

    Color Red = new Color32(159, 54, 13, 180);
    Color Orange = new Color32(234, 94, 17, 180);
    Color Yellow = new Color32(255,250,60,180);
    Color Green = new Color32(46,154,19,180);
    Color Blue = new Color32(13, 29, 195, 180);
    Color DeepBlue = new Color32(54, 13, 159, 180);
    Color Pink = new Color32(246,14,240,180);
    Color SkyBlue = new Color32(28, 83, 212, 180);
    Color Brown = new Color32(116, 81, 19, 180);
    public Color SaveColor;
    public bool Saved = false;
    public Text tx;
    public Light lt;

    void Start()
    {
        Button_Red();
    }

    public void Button_Red()
    {
        tx.text = "Red";
        tx.color = Red; 
        SaveColor = Red;
        ChangeColor(Red);
        Saved = true;
    }
    public void Button_Orange()
    {
        tx.text = "Orange";
        tx.color = Orange;
        SaveColor = Orange;
        ChangeColor(Orange);
        Saved = true;
    }
    public void Button_Yellow()
    {
        tx.text = "Yellow";
        tx.color = Yellow;
        SaveColor = Yellow;
        ChangeColor(Yellow);
        Saved = true;
    }
    public void Button_Green()
    {
        tx.text = "Green";
        tx.color = Green;
        SaveColor = Green;
        ChangeColor(Green);
        Saved = true;
    }
    public void Button_Blue()
    {
        tx.text = "Blue";
        tx.color = Blue;
        SaveColor = Blue;
        ChangeColor(Blue);
        Saved = true;
    }
    public void Button_Purple()
    {
        tx.text = "Deep Blue";
        tx.color = DeepBlue;
        SaveColor = DeepBlue;
        ChangeColor(DeepBlue);
        Saved = true;
    }
    public void Button_Pink()
    {
        tx.text = "Pink";
        tx.color = Pink;
        SaveColor = Pink;
        ChangeColor(Pink);
        Saved = true;
    }
    public void Button_White()
    {
        tx.text = "SkyBlue";
        tx.color = SkyBlue;
        SaveColor = SkyBlue;
        ChangeColor(SkyBlue);
        Saved = true;
    }
    public void Button_Brown()
    {
        tx.text = "Brown";
        tx.color = Brown;
        SaveColor = Brown;
        ChangeColor(Brown);
        Saved = true;
    }

    public void ChangeColor(Color co)
    {
        ParticleSystem[] ParticleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
        lt.color = co;


        foreach (ParticleSystem _ParticleSystem in ParticleSystems)
        {
            _ParticleSystem.startColor = co;
        }
    }
}
