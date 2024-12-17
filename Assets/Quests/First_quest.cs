using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_quest
{
    public string Description { get; set; }
    public bool Completed { get; set; }
        
    public void Complete ()
    {
        Completed = true;    
    }

}
