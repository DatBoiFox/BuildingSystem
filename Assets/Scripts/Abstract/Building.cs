using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    // Public, because Load/Save Plugin does not work otherwise.

    protected Collider collider;
    private Renderer renderer;

    public Color color;
    public Vector3 position;
    public Vector3 rotation;
    public string prefabName;
    public int layer;

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
    public void SetColor(Color color)
    {
        renderer.material.SetColor("_Color", color);
    }
    public void ApplyColor(Color color)
    {
        renderer.material.SetColor("_Color", color);
        this.color = color;
    }
    public Color GetColor()
    {
        return color;
    }
    public void ResetColor()
    {
        renderer.material.SetColor("_Color", this.color);
    }
    public void SetRotation(Vector3 rotation)
    {
        Debug.Log("Rotation " + rotation);
        transform.rotation = Quaternion.Euler(rotation);
        this.rotation = transform.rotation.eulerAngles;

    }
    public void SetLayer(int layer)
    {
        this.gameObject.layer = layer;
        this.layer = layer;
    }
    public void LoadConstructor(Vector3 position, Vector3 rotation, Color color, int layer)
    {
        SetRotation(rotation);
        SetPosition(position, true);
        ApplyColor(color);
        SetLayer(layer);
    }

}
