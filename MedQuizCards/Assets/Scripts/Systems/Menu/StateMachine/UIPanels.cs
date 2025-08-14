using Nato.UI;
using UnityEngine;

namespace Nato.StateMachine
{
    public class UIPanels : MonoBehaviour
    {
        [field: Header("Global UI")]
        [field: SerializeField] public AudioUI AudioUI {get; private set;}
    }
}
