using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    // Public, because Load/Save Plugin does not work otherwise.

    // Name of the building
    public string Name;

    protected Collider collider;
    private Renderer renderer;

    // Base variables for the building
    public Color color;
    public Vector3 position;
    public Vector3 rotation;
    public int layer;
    public bool canBePlacedOnWall = true;

    public string prefabName;

    private void Awake()
    {
        collider = this.GetComponent<Collider>();
        renderer = this.GetComponent<Renderer>();
        color = renderer.material.GetColor("_Color");
    }

    public virtual void SetPosition(Vector3 position, bool onLoad = false)
    {
        this.transform.position = position;
        this.position = position;
    }
    public Collider GetCollider()
    {
        return collider;
    }
    /// <summary>
    /// Sets color for representation, not permanently applying it.
    /// </summary>
    /// <param name="color">Color that will be used to paint the object</param>
    public void SetColor(Color color)
    {
        renderer.material.SetColor("_Color", color);
    }
    /// <summary>
    /// Applies selected color
    /// </summary>
    /// <param name="color">Color that will be used to paint the object</param>
    public void ApplyColor(Color color)
    {
        renderer.material.SetColor("_Color", color);
        this.color = color;
    }
    public Color GetColor()
    {
        return color;
    }
    /// <summary>
    /// Resets color to its original color at that time
    /// </summary>
    public void ResetColor()
    {
        renderer.material.SetColor("_Color", this.color);
    }
    public void SetRotation(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
        this.rotation = transform.rotation.eulerAngles;

    }
    public void SetLayer(int layer)
    {
        this.gameObject.layer = layer;
        this.layer = layer;
    }

    /// <summary>
    /// Constructor-like function used for loading game objects from save file
    /// </summary>
    /// <param name="position">Position of the object</param>
    /// <param name="rotation">Rotation of the object</param>
    /// <param name="color">Object's color</param>
    /// <param name="layer">Object's layer</param>
    public void LoadConstructor(Vector3 position, Vector3 rotation, Color color, int layer)
    {
        SetRotation(rotation);
        SetPosition(position, true);
        ApplyColor(color);
        SetLayer(layer);
    }

}
