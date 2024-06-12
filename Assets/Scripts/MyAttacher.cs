using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;


//Grab�ϸ� �ָ��� ����� �ִ°� �ƴ϶� ���տ� �ٵ��� �ϴ� ���
public class MyAttacher : MonoBehaviour
{
    private IXRSelectInteractable _selectInteractable;

    protected void OnEnable()
    {
        /*
        XRGrabInteractable�� GetComponent�ϸ� ���̴�. �츮�� Grabable ������Ʈ�� ��������� �˸� �ȴ�. 
        XRGrabInteractable�� XRBaseInteractable�� ��ӹް� �ְ�,
        XRBaseInteractable�� IXRSelectInteractable�� ��ӹް� �ִ�.
        IXRSelectInteractable�� ����� �� ȣ��Ǵ� OnSelectEntered�� ������ �ִ�.
        XRGrabInteractable�� ���ؼ� IXRSelectInteractable�� GetComponent �� �� �ִ�.
         */

        _selectInteractable = this.GetComponent<IXRSelectInteractable>();
        if( _selectInteractable as Object == null)
        {
            /*
            as Object ĳ����
            as �����ڴ� ������ �� ��ȯ�� ������. ��ȯ ������ ��ȯ�� ��ü��, ���н� null�� ��ȯ��.
            �׷��� �� ���ǹ��� 2���� ��쿡 true�� ��ȯ��.
            1. _selectInteractable�� null�� ���. (�̰� �׳� _selectInteractable == null ���ǰ� ����)
            2. IXRSelectInteractable�� �����ϴ� Unity��ü�� �������� �ʾƼ� Object�� ��ȯ�� �� ���� ���.
            (�� ���� Unity��ü�� �ı��� ��쿡�� �ùٸ��� null�� ��ȯ�� �� ����.)
            ��� �� �𸣰���.
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
        //Interactor�� ���� ��, Interactable�� �ֿ��� ����.
        if ((args.interactorObject is XRRayInteractor) == false) return;

        //attachTransform�� _selectInteractable�� ���� ��ġ
        var attachTransform = args.interactorObject.GetAttachTransform(_selectInteractable);
        //originAttachPos�� _selectInteractable�� ���� ���� ���� ��ġ
        //������Ʈ�� ó�� ������ ���� ��ġ�� ȸ�� ������ �ǹ��Ѵ�. ������Ʈ�� ���� ��ǥ�踦 �������� �Ѵ�.
        //������Ʈ�� ���� �����Ǿ����� ���� ���·� �����ϴ� �� ���� �� �ִ�.
        var originAttachPos = args.interactorObject.GetLocalAttachPoseOnSelect(_selectInteractable);
        //���߿� �����غ���
        attachTransform.SetLocalPose(originAttachPos);
    }
}
