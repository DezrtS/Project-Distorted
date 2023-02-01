using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] private float destroyAfter;

    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
}
