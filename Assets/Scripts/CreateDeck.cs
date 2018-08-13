using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateDeck
{

    [MenuItem("Assets/Create/Deck List")]
    public static Deck Create()
    {
        Deck asset = ScriptableObject.CreateInstance<Deck>();

        AssetDatabase.CreateAsset(asset, "Assets/Deck.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
