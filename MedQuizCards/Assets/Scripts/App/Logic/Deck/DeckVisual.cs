using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    [RequireComponent(typeof(Deck))]
    public class DeckVisual : MonoBehaviour
    {
        private Deck deck;

        [field: Header("Texts")]
        [field: SerializeField] public TextMeshProUGUI DeckTitleText { get; private set; }
        [field: SerializeField] public Image DeckImage { get; private set; }

        [SerializeField] private GameObject[] cards;
        [SerializeField] private float deckMountPeriod = 0.1f;

        private IEnumerator deckMountCoroutine;


        private void OnEnable()
        {
            transform.localEulerAngles = new Vector3(0, 0, Random.Range(-3f, 3f));
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(false);
            }
            StartCoroutine(DeckMountCoroutine());

        }

        private IEnumerator DeckMountCoroutine()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                yield return new WaitForSeconds(deckMountPeriod);
                cards[i].gameObject.SetActive(true);
            }
        }

        private void Awake()
        {
            deck = GetComponent<Deck>();
            Setup();
        }

        public void Setup()
        {
            DeckTitleText.SetText(deck.ProcedureDeck.procedureName);
            DeckImage.sprite = deck.ProcedureDeck.procedureImage;
        }
    }
}
