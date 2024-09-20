using UnityEngine;

public class SOTable : MonoBehaviour
{
    private BlueprintViewer bpViewer;
    public SO_ItemBlueprint bluePrint;

    private void OnEnable()
    {
        bpViewer = FindObjectOfType<BlueprintViewer>();
    }

    public void SetCurrentBP()
    {
        SO_ItemBlueprint bp = Instantiate(bluePrint);
        
        bpViewer.SetCurrentItemBlueprint(bp);
    }
}
