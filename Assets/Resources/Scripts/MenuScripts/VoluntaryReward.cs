using UnityEngine;

public class VoluntaryReward : MonoBehaviour
{
    private static bool VoluntaryExecuted = false;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(VoluntaryExecuted);
    }

    public void VoluntaryRewad()
    {
        VoluntaryExecuted = true;
        Start();
    }
}
