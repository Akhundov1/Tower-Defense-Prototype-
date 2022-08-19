using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MeshController : MonoBehaviour
{
    public List<MeshRenderer> Renderers;
    Dictionary<MeshRenderer, Color> OriginalColors = new Dictionary<MeshRenderer, Color>();
    public TowerController mainController;
    public Color MainColor = Color.white;


    public void MouseOnMesh(bool Status)
    {
        if (Status)
        {
            ChangeColor(false, Color.red);
        }
        else
        {
            ChangeColor(true, Color.green);
        }
    }

    public void InitColorDictionary()
    {
        Dictionary<Color, int> AllColors = new Dictionary<Color, int>();
        List<Color> ColorsInMaterials = new List<Color>();
        foreach (var item in Renderers)
        {
            OriginalColors.Add(item, item.material.color);
            if (AllColors.ContainsKey(item.material.color))
            {
                AllColors[item.material.color] += 1;
            }
            else
            {
                AllColors.Add(item.material.color,1);
                ColorsInMaterials.Add(item.material.color);
            }
        }
        int Biggest = 0;
        foreach (var item in ColorsInMaterials)
        {
            if (AllColors[item] > Biggest)
            {
                Biggest = AllColors[item];
                MainColor = item;
            }
        }

        
    }
    public void ChangeColor(bool IsOriginal, Color newColor)
    {
        if (Renderers != null && OriginalColors != null)
        {
            if (IsOriginal)
            {
                foreach (var item in Renderers)
                {
                    if (item != null)
                    {
                        item.material.color = OriginalColors[item];

                    }

                }
            }
            else
            {
                foreach (var item in Renderers)
                {
                    if (item != null)
                    {
                        item.material.color = newColor;
                    }

                }

            }
        }
    }
    public void ClearColors()
    {
        Renderers.Clear();
        OriginalColors.Clear();
    }
    public Color GetMainColor()
    {
        if(MainColor != null)
        {
            return MainColor;
        }
        return Color.white;
    }
}
