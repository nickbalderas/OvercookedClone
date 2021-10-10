using UnityEngine;

public class CounterUnit : MonoBehaviour
{
    private bool _isPlayerNear;
    private bool _isPlayerFacing;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNear)
        {
        }
        
        if (_isPlayerFacing)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isPlayerNear = other.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerNear = false;
        _isPlayerFacing = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerFacing = Vector3.Dot(other.transform.forward, (transform.position - other.transform.position).normalized) > 0;
        }
    }
}
