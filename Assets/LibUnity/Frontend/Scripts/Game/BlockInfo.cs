using Bencodex.Types;
using Libplanet.Assets;
using Libplanet.Blocks;
using LibUnity.Backend;
using LibUnity.Backend.State;
using LibUnity.Frontend;
using TMPro;
using UnityEngine;
using UniRx;
using ObservableExtensions = UniRx.ObservableExtensions;

public class BlockInfo : MonoBehaviour
{
    private TextMeshProUGUI informationText;
    private long _blockIndex;
    private BlockHash _hash;
    private Currency _currency;
    
    void Awake()
    {
        informationText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Debug.Log("start");
        ObservableExtensions.Subscribe(Game.Instance.Agent.BlockIndexSubject, SubscribeBlockIndex).AddTo(gameObject);
        ObservableExtensions.Subscribe(Game.Instance.Agent.BlockTipHashSubject, SubscribeBlockHash).AddTo(gameObject);
    }

    private void SubscribeBlockIndex(long blockIndex)
    {
        _blockIndex = blockIndex;
        UpdateText();
    }

    private void SubscribeBlockHash(BlockHash hash)
    {
        _hash = hash;
        UpdateText();
    }

    private void UpdateText()
    {
        Debug.Log("UpdateText");
        
        informationText.text = $"Block index : <color=#FF0000>{_blockIndex}</color> / Hash: <color=#FF0000>{_hash.ToString()}</color>";;
    }
}
