using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [HideInInspector]
    public int[] location;

    private bool alive = false;
    private Image image;
    [SerializeField] private Color aliveColor, deadColor;

    private Manger manger;

    private void Start()
    {

        image = GetComponent<Image>();
    }

    public void ChangeState()
    {
        if(manger == null)
            manger = GameObject.FindWithTag("Manger").GetComponent<Manger>();


        alive = !alive;
        if (alive)
        {
            manger.AddCell(this);
            image.color = aliveColor;
        }
        else
        {
            manger.RemoveCell(this);
            image.color = deadColor;
        }
    }

    public bool GetState()
    {
        return alive;
    }

}
