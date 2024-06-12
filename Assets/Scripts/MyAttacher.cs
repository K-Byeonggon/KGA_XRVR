using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;


//Grab하면 멀리서 붙잡고 있는게 아니라 내손에 붙도록 하는 기능
public class MyAttacher : MonoBehaviour
{
    private IXRSelectInteractable _selectInteractable;

    protected void OnEnable()
    {
        /*
        XRGrabInteractable을 GetComponent하면 무겁다. 우리는 Grabable 오브젝트를 잡았을때만 알면 된다. 
        XRGrabInteractable은 XRBaseInteractable을 상속받고 있고,
        XRBaseInteractable은 IXRSelectInteractable을 상속받고 있다.
        IXRSelectInteractable은 잡았을 때 호출되는 OnSelectEntered를 가지고 있다.
        XRGrabInteractable을 통해서 IXRSelectInteractable을 GetComponent 할 수 있다.
         */

        _selectInteractable = this.GetComponent<IXRSelectInteractable>();
        if( _selectInteractable as Object == null)
        {
            /*
            as Object 캐스팅
            as 연산자는 안전한 형 변환을 수행함. 변환 성공시 변환된 객체를, 실패시 null을 반환함.
            그래서 위 조건문은 2가지 경우에 true를 반환함.
            1. _selectInteractable이 null인 경우. (이건 그냥 _selectInteractable == null 조건과 같음)
            2. IXRSelectInteractable을 구현하는 Unity객체가 존재하지 않아서 Object로 변환될 수 없는 경우.
            (이 경우는 Unity객체가 파괴된 경우에도 올바르게 null을 반환할 수 있음.)
            사실 잘 모르겠음.
            */

            Debug.LogError("Attacher Need SelectInteractable");
            return;
        }

        _selectInteractable.selectEntered.AddListener(OnSelectEntered);
    }
    protected void OnDisable()
    {
        _selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        //Interactor는 주은 놈, Interactable은 주워진 것임.
        if ((args.interactorObject is XRRayInteractor) == false) return;

        //attachTransform은 _selectInteractable의 부착 위치
        var attachTransform = args.interactorObject.GetAttachTransform(_selectInteractable);
        //originAttachPos는 _selectInteractable의 원래 로컬 부착 위치
        //오브젝트가 처음 잡혔을 때의 위치와 회전 정보를 의미한다. 오브젝트의 로컬 좌표계를 기준으로 한다.
        //오브젝트가 선택 해제되었을떄 원래 상태로 복원하는 데 사용될 수 있다.
        var originAttachPos = args.interactorObject.GetLocalAttachPoseOnSelect(_selectInteractable);
        //나중에 이해해보자
        attachTransform.SetLocalPose(originAttachPos);
    }
}
