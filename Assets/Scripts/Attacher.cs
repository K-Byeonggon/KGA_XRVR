using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class Attacher : MonoBehaviour
{
    private IXRSelectInteractable _selectInteractable;
    protected void OnEnable()
    {
        _selectInteractable = this.GetComponent<IXRSelectInteractable>();
        if( _selectInteractable as Object == null)
        {
            Debug.LogError("Attacher Need SelectInteractable");
            return;
        }

        _selectInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    protected void OnDisable()
    {
        if( _selectInteractable as Object != null )
        {
            _selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (false == (args.interactorObject is XRRayInteractor))
            return;

        var attachTransform = args.interactorObject.GetAttachTransform(_selectInteractable);
        var originAttachPos = args.interactorObject.GetLocalAttachPoseOnSelect(_selectInteractable);
        attachTransform.SetLocalPose(originAttachPos);
    }
}
