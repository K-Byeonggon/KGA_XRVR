using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using System.Runtime.CompilerServices;

//locomotion System 직접 수정 안하게 매니저 만들기
public class LocomoteManager : MonoBehaviour
{
    public enum MoveStyleType { HeadRelative, HandRelative }
    public enum TurnStyleType { Snap, Continuous }


    [Header("XR")]
    [SerializeField] XROrigin XROrigin;
    [SerializeField] XRBaseController Controller_Left;
    [SerializeField] XRBaseController Controller_Right;

    [Header("Locomotion")]
    [SerializeField] ContinuousMoveProviderBase Provider_ContinuousMove;
    [SerializeField] ContinuousTurnProviderBase Provider_ContinuousTurn;
    [SerializeField] SnapTurnProviderBase Provider_SnapTurn;
    [SerializeField] TeleportationProvider Provider_Teleportation;

    [Header("Property")]
    [SerializeField] MoveStyleType _leftHandMoveStyle;
    [SerializeField] TurnStyleType _rightHandTurnStyle;
    [SerializeField, Range(0.0f, 5.0f)] float _moveSpeed;
    [SerializeField] bool _isEnableStrafe;
    [SerializeField] bool _isUseGravity;
    [SerializeField] bool _isEnableFly;
    [SerializeField, Range(0.0f, 180.0f)] float _turnSpeed;
    [SerializeField] bool _isEnableTurnAround;
    [SerializeField, Range(0.0f, 90.0f)] float _snapTurnAmount;



    /*구버전 Getter함수, Setter함수
    public MoveStyleType GetMoveStyle()
    {
        return _leftHandMoveStyle;
    }

    public void SetMoveStyle(MoveStyleType val)
    {
        _leftHandMoveStyle = val;
    }
    */

    public MoveStyleType MoveStyle
    {
        get { return _leftHandMoveStyle; }
        set
        {
            _leftHandMoveStyle = value;
            //set에서 값변경 뿐만아니라 이벤트 처럼 활용가능.
            //값이 변경되는 시점에 디버깅하기 편함.
            switch(_leftHandMoveStyle)
            {
                case MoveStyleType.HeadRelative:
                    Provider_ContinuousMove.forwardSource = XROrigin.Camera.transform;
                    break;
                case MoveStyleType.HandRelative:
                    Provider_ContinuousMove.forwardSource = Controller_Left.transform;
                    break;

            }
        }
    }

    public TurnStyleType TurnStyle
    {
        get { return _rightHandTurnStyle; }
        set
        {
            _rightHandTurnStyle = value;

            switch (_rightHandTurnStyle)
            {
                case TurnStyleType.Snap:
                    Provider_ContinuousTurn.enabled = false;
                    Provider_SnapTurn.enabled = true;
                    break;
                case TurnStyleType.Continuous:
                    Provider_ContinuousTurn.enabled = true;
                    Provider_SnapTurn.enabled = false;
                    break;
            }
        }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set
        {
            _moveSpeed = value;
            Provider_ContinuousMove.moveSpeed = _moveSpeed;
        }
    }

    public bool IsEnableStrafe
    {
        get { return _isEnableStrafe;}
        set 
        { 
            _isEnableStrafe = value;
            Provider_ContinuousMove.enableStrafe = _isEnableStrafe;
        }
    }

    public bool IsUseGravity
    {
        get { return _isUseGravity; }
        set 
        {
            _isUseGravity = value;
            Provider_ContinuousMove.useGravity = _isUseGravity;
        }
    }

    public bool IsEnableFly
    {
        get { return _isEnableFly; }
        set
        {
            _isEnableFly = value;
            Provider_ContinuousMove.enableFly = _isEnableFly;
        }
    }

    public float TurnSpeed
    {
        get { return _turnSpeed; }
        set
        {
            _turnSpeed = value;
            Provider_ContinuousTurn.turnSpeed = _turnSpeed;
        }
    }

    public bool IsEnableTurnAround
    {
        get { return _isEnableTurnAround; }
        set 
        {
            _isEnableTurnAround = value;
            Provider_SnapTurn.enableTurnAround = _isEnableTurnAround;
        }
    }

    public float SnapTurnAmount
    {
        get { return _snapTurnAmount; }
        set
        {
            _snapTurnAmount = value;
            Provider_SnapTurn.turnAmount = _snapTurnAmount;
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var aaa = MoveSpeed;


        MoveStyle = _leftHandMoveStyle;
        TurnStyle = _rightHandTurnStyle;
        MoveSpeed = _moveSpeed;
        IsEnableStrafe = _isEnableStrafe;
        IsUseGravity = _isUseGravity;
        IsEnableFly = _isEnableFly;
        TurnSpeed = _turnSpeed;
        IsEnableTurnAround = _isEnableTurnAround;
        SnapTurnAmount = _snapTurnAmount;
    }

}
