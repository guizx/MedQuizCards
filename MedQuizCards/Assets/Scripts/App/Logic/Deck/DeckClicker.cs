using UnityEngine;
using UnityEngine.InputSystem;

namespace MedQuizCards
{
    public class DeckClicker : MonoBehaviour
    {
        private Camera cam;

        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            if (!Deck.IsClicklable)
                return;

            Vector2 screenPos;

            // PC 
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                screenPos = Mouse.current.position.ReadValue();
                RaycastFromScreen(screenPos);
            }

            // Mobile
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
                RaycastFromScreen(screenPos);
            }
        }

        private void RaycastFromScreen(Vector2 screenPos)
        {
            Ray ray = cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Deck deck))
                {
                    QuizManager.Instance?.SetCurrentQuestion(deck.ProcedureDeck, deck.GetQuestion());
                }
            }
        }
    }
}
