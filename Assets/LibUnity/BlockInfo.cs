using System;
using Libplanet.Blocks;
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
        informationText.text = $"Block index [{_blockIndex}] / Hash: [{_hash.ToString()}]";;
    }
}
