using UnityEngine;

public class CanvasGroupRevealer : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool startHidden = false;
    [SerializeField] private float shownAlpha = 1;
    [SerializeField] private bool shownBlockRaycast = true;
    [SerializeField] private bool shownInteractable = true;

    private bool shown = true;

    private void Start()
    {
        if (startHidden)
        {
            HideGroup();
        }
    }

    public void Toggle()
    {
        if (shown)
        {
            HideGroup();
        }
        else
        {
            ShowGroup();
        }
    }

    public void ShowGroup()
    {
        shown = true;
        canvasGroup.alpha = shownAlpha;
        canvasGroup.blocksRaycasts = shownBlockRaycast;
        canvasGroup.interactable = shownInteractable;
    }

    public void HideGroup()
    {
        shown = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
