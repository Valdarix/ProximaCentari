
using UnityEngine;
using UnityEngine.Playables;



public class TimelineController : MonoBehaviour
{
    private PlayableDirector _director;

    [SerializeField] private GameObject[] _flightPlans;
    private int _currentFlightPlan;

    void Start()
    {
        _director = GetComponent<PlayableDirector>();
    }

    public void NextPattern()
    {
        if (_director == null)
        {
            _director = GetComponent<PlayableDirector>();
        }
        
        foreach (var go in _flightPlans)
        {
            go.SetActive(false);
        }
        
        var random = Random.Range(0, _flightPlans.Length -1);
        if (_currentFlightPlan == random) // prevent the same fligh path
        {
            random = random == 0 ? 1 : 0;
        }
        
        for (var i = 0; i < _flightPlans.Length; i++)
        {
            _flightPlans[i].SetActive(i == random);
            if (_flightPlans[i].activeInHierarchy)
            {
                _currentFlightPlan = i;
            }
            
        }

    }
}
