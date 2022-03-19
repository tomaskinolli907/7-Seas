using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Gold { get; set; }
    public int Health { get; set; }
    public int Team { get; set; } // 1 is red, 2 is blue
    public int Mutiny { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public bool IsFighting { get; set; }
}