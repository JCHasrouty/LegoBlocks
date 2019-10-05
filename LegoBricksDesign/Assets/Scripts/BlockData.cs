using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    singleUnit,
    doubleUnit
}

public class BlockData : MonoBehaviour
{
    public BlockType Type = BlockType.singleUnit;

    [Range(1, 6)]
    public int Size = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
