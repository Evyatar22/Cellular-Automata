using UnityEngine;
using UnityEngine.UI;

public class Neuron : MonoBehaviour
{
    [HideInInspector]
    public int[] location;

    private int state = 0;
    private Image image;
    [SerializeField] private Color aliveColor, deadColor, dyingColor1, dyingColor2;
    [SerializeField] private bool rndState = false;

    private BrianManger manger;

    private void Start()
    {
        image = GetComponent<Image>();
        manger = GameObject.FindGameObjectWithTag("Manger").GetComponent<BrianManger>();

        
    }

    public void ChangeState()
    {
        state++;

        if(state == 1)
        {
            image.color = aliveColor;
            manger.AddCell(this);
        }
        else if(state == 2)
        {
            if(Random.Range(0,2) == 1)
                image.color = dyingColor2;
            else
                image.color = dyingColor1;
        }
        else if(state == 3)
        {
            state = 0;
            manger.RemoveCell(this);
            image.color = deadColor;
        }
    }

    public int GetState() { return state; }
}
