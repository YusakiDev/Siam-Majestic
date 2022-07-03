

public class SelectionUI : Singleton<SelectionUI>
{
    public bool mouseIsOverUI;
    private void OnMouseOver()
    {
        mouseIsOverUI = true;
    }

    private void OnMouseExit()
    {
        mouseIsOverUI = false;
    }
}
