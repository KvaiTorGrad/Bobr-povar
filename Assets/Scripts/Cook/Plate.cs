using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private IngradientIDName _ingradientID;

    public void SetIngradientCooking()
    {
        GameManager.Instance.GetIngradient(_ingradientID, out GameObject fantom);
        CreateFantomIngradient(fantom);
    }

    private void CreateFantomIngradient(GameObject fantom)
    {
        Settings.Instance.PlayOneShotClip(0);
        var spriteFantom = Instantiate(fantom, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>();
        spriteFantom.sortingOrder = 67;
    }
}
