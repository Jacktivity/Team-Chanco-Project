using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class levels : MonoBehaviour
{

}

public class mapline : MonoBehaviour
{
    [XmlAttribute("value")]
    public int[] value;
}

public class map : MonoBehaviour
{
    [XmlAttribute("layer")]
    public int layer;

    [XmlAttribute("placementpoints")]
    public int placementpoints;

    [XmlArray("mapline")]
    public mapline maplines_object;
}

public class maps : MonoBehaviour
{
    [XmlArray("map")]
    public map maps_object;
}

public class rotationline : MonoBehaviour
{
    [XmlAttribute("value")]
    public int[] value;
}

public class rotation : MonoBehaviour
{
    [XmlAttribute("layer")]
    public int layer;

    [XmlArray("rotationline")]
    public rotationline rotationline_object;
}

public class rotations : MonoBehaviour
{
    [XmlArray("rotation")]
    public rotation rotation_object;
}

public class enemy : MonoBehaviour
{

}