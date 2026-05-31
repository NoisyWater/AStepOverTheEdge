using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] 
    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}