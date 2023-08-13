using UnityEngine;

public class VoluntaryReward : MonoBehaviour
{
    private bool _voluntaryExecuted = false;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(_voluntaryExecuted);
    }

    public void VoluntaryRewad()
    {
        _voluntaryExecuted = true;
        Start();
    }
}
