using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject square;
    [SerializeField]
    int _height, _length;

    // Start is called before the first frame update
    void Start()
    {

        for (int k = 0; k < _length; k++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameObject block = Instantiate(square, new Vector2((k - _length / 2), (j - _height / 2)), Quaternion.identity, transform);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
