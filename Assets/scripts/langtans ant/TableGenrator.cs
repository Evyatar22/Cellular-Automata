using UnityEngine;
using UnityEngine.UI;

public class TableGenrator : MonoBehaviour
{
    #region vars
    [Header("Must have prefabs:")]
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image line, box;

    [Header("user based vars:")]
    [SerializeField] private bool conwaysGame = false, brians = false;
    [SerializeField] private float distance = 1f;
    public Color tileColor;
    [SerializeField] private Vector2 boxSize = new Vector2(15f, 15f);


    int width, height;
    private Image[,] tileMap;

    #endregion

    void Awake()
    {
        GenrateTable();
    }

   
    private void GenrateTable()
    {
        float dis = boxSize.x + distance;
        width = (int)(canvas.sizeDelta.x / (boxSize.x + distance)) + 1;
        height = (int)(canvas.sizeDelta.y / (boxSize.y + distance)) + 1;

     

        tileMap = new Image[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Image img = Instantiate(box, new Vector3(x * dis, y * dis, 0f), transform.rotation);
                img.rectTransform.SetParent(canvas.GetComponent<RectTransform>());
                img.rectTransform.sizeDelta = boxSize;
                img.color = tileColor;
                tileMap[x, y] = img;

                if(conwaysGame)
                    img.GetComponent<Cell>().location = new int[] { x, y };
                else if(brians)
                {
                    img.GetComponent<Neuron>().location = new int[] { x, y };
                }

            }
            GenerateLine(new Vector3(x * dis + boxSize.x / 2 + distance / 2, canvas.sizeDelta.y / 2f, 0f), true);

        }

        for (int y = 0; y < height; y++)
        {
            GenerateLine(new Vector3(canvas.sizeDelta.x / 2f, y * dis + boxSize.y / 2 + distance / 2, 0f), false);
        }
    }

    private void GenerateLine(Vector3 pos, bool side)
    {
        if (side)
        {
            Image line2 = Instantiate(line, pos, transform.rotation);
            line2.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, canvas.sizeDelta.y);
            line2.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
            return;
        }
        Image line1 = Instantiate(line, pos, new Quaternion(0f, 0f, 90f, 0f));
        line1.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.sizeDelta.x, distance);
        line1.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
    }
    public Image[,] GetTileData()
    {
        return tileMap;
    }
}

