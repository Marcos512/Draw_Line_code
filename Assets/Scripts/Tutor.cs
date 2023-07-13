using UnityEngine;

class Tutor : MonoBehaviour
{
    [SerializeField]
    private Game _game;

    [SerializeField]
    private GameObject _pointer;

    [SerializeField]
    private Transform _button;

    private void Update()
    {
        if (_game.LineIsDraw)
        {
            _pointer.GetComponent<BezierMover>().enabled = false;
            _pointer.transform.position = _button.position + new Vector3(0.2f,0.2f) ;
        }
    }

    public void HideTutor()
    {
        _pointer.SetActive(false);
    }
}

