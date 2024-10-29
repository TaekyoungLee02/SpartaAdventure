using TMPro;
using UnityEngine;

public class UIDescription : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    private PlayerInteraction playerInteraction;
    private IInteractiveObject interactiveObject;
    private GameObject hitObject;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = CharacterManager.Instance.Player.interaction;
    }

    // Update is called once per frame
    void Update()
    {
        hitObject = playerInteraction.hitObject;

        if (hitObject != null)
        {
            interactiveObject = hitObject.GetComponent<IInteractiveObject>();

            if (interactiveObject != null)
            {
                title.text = interactiveObject.Title;
                description.text = interactiveObject.Description;
                return;
            }
        }

        title.text = "";
        description.text = "";
    }
}
