using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PrepareFire : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    GameObject Ball;
    Vector3 newPos;
    [SerializeField]
    GameObject Nong;
    [SerializeField]
    GameObject Nongsung;
    public GameObject Goal1;
    public GameObject Goal2;
    public GameObject Goal3;
    public GameObject End;
    public Button button;

    // Start is called before the first frame update
    void UpdateSlider()
    {
        slider.value = slider.value+Time.deltaTime*3;
        if(slider.value == slider.maxValue) slider.value = 0;
    }
    Vector3 CrosshairPosition()
    {
        newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
        newPos.z = transform.position.z;
        transform.position = newPos;
        Nong.transform.eulerAngles=new Vector3(newPos.x*10,90,45);
        return newPos;
    }

    void Fire(Vector3 newPos)
    {
        GameObject BallClone =Instantiate(Ball, Nongsung.transform.position, Quaternion.identity);
        var sequence = DOTween.Sequence();
  
        Vector3 FireNeedDestroy = new Vector3(newPos.x, newPos.y, slider.value);
        sequence.Append(BallClone.transform.DOMove(FireNeedDestroy,3f,false));
        sequence.Append(BallClone.transform.DOMove(new Vector3(1000,1000,1000), 0.000001f, false));

        slider.value = 0;
    }
    void Start()
    {
        button.onClick.AddListener(Onclick);
    }
    void Onclick() { End.SetActive(false); }
    // Update is called once per frame
    void Update()
    {
        CrosshairPosition();
        if (Input.GetMouseButton(0)) UpdateSlider();
        if(Input.GetMouseButtonUp(0)){
            Fire(newPos);
        }
        if (Goal1 == null && Goal2 == null && Goal3 == null)
        {
            End.SetActive(true);
        }
       
    }
   
}
