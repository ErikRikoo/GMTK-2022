using System.Collections;
using System.Collections.Generic;
using GMTK;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Player_holder player_holder;

    
    // Start is called before the first frame update
    void Start()
    {
        player_holder.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
