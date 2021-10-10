using UnityEngine;

public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    public void HighlightInteractable(bool indicator)
    {
        Color color = indicator ? Color.gray : Color.clear;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        
    }
}
