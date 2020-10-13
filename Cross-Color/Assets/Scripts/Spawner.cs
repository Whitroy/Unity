using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    public GameObject[] Shape;

    public GameObject ColorChanger;

    public int No_of_Shape_in_Scene = 0;

    public int Max_Number_of_shape = 2;

    float lastSpwanPosY;

    bool resetflag = false;

    List<Transform> shapeTransforms;
    float gap = 8f;
    // Start is called before the first frame update
    void Awake()
    {
        lastSpwanPosY = 1f;
        shapeTransforms = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (No_of_Shape_in_Scene < Max_Number_of_shape && GameManager.gameManager.GetGameState()==GameState.Play)
        {
            Debug.Log("Spawn");
            No_of_Shape_in_Scene++;
            resetflag = true;
            lastSpwanPosY += gap;
            GameObject newshape = Instantiate<GameObject>(Shape[Random.Range(0, Shape.Length)], new Vector3(0f, lastSpwanPosY, 0f), Quaternion.identity);
            Instantiate<GameObject>(ColorChanger, new Vector3(0f, lastSpwanPosY - 4f, 0f), Quaternion.identity);
            shapeTransforms.Add(newshape.transform);
        }

        if (shapeTransforms.Count>0 &&GameObject.FindGameObjectWithTag("Player").transform.position.y > shapeTransforms[0].position.y+5f && GameManager.gameManager.GetGameState() == GameState.Play)
        {
            GameObject del =shapeTransforms[0].gameObject;
            shapeTransforms.RemoveAt(0);
            Destroy(del);
            No_of_Shape_in_Scene--;
        }

        if (GameManager.gameManager.GetGameState() == GameState.GameOver)
        {
            Reset();
        }
    }

    private void Reset()
    {
        if (resetflag)
        {
            resetflag = false;
            lastSpwanPosY = 1f;
            No_of_Shape_in_Scene = 0;
            foreach(Transform transform in shapeTransforms)
            {
                GameObject del = transform.gameObject;
                Destroy(del);
            }

            GameObject[] obj= GameObject.FindGameObjectsWithTag("ColorChanger");

            foreach(GameObject gameObject in obj)
            {
                Destroy(gameObject);
            }

            obj = null;

            shapeTransforms.RemoveRange(0,shapeTransforms.Count);
            shapeTransforms = new List<Transform>();
        }
    }
}
