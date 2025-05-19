using UnityEngine;
using UnityEngine.UI;

public class Ant : MonoBehaviour
{
    public int[] position = new int[2];

    int num;
    public float angle = 90f;
    private RectTransform myTr;

    public Color[] colorSequence = new Color[4];
    public float[] anglesAddition = new float[4];

    private GameManger manger;
    public RectTransform pointer;

    private void Start()
    {
        

        myTr = GetComponent<RectTransform>();
        manger = GameObject.Find("GameManger").GetComponent<GameManger>();
    }
    public void SetPos(int[] pos)
    {
        position = pos;
    }
    public void SetAngle(float angl)
    { 
        angle = angl;
        myTr.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
    public void SetNum(int i)
    {
        num = i;
    }
   

   
    public void NextMove()
    {

        try
        {
            manger.UpdateImage(colorSequence, this);
        }
        catch
        {
            manger.GiveNewPos(this);
        }
    }
}
