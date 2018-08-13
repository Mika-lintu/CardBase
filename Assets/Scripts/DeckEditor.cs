using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeckEditor : EditorWindow
{

    public Deck deckList;
    private int viewIndex = 1;

    [MenuItem ("Window/Deck Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(DeckEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            deckList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(Deck)) as Deck;
        }    
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Deck Editor", EditorStyles.boldLabel);
        if (deckList != null)
        {
            if (GUILayout.Button("Show Deck"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = deckList;
            }
        }
        if (GUILayout.Button("Open Deck"))
        {
            OpenlistOfCards();
        }
        if (GUILayout.Button("New Deck"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = deckList;
        }
        GUILayout.EndHorizontal();

        if (deckList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Deck", GUILayout.ExpandWidth(false)))
            {
                CreateNewlistOfCards();
            }
            if (GUILayout.Button("Open Existing Deck", GUILayout.ExpandWidth(false)))
            {
                OpenlistOfCards();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (deckList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < deckList.listOfCards.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Card", GUILayout.ExpandWidth(false)))
            {
                AddCard();
            }
            if (GUILayout.Button("Delete Card", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (deckList.listOfCards == null)
                Debug.Log("wtf");
            if (deckList.listOfCards.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card", viewIndex, GUILayout.ExpandWidth(false)), 1, deckList.listOfCards.Count);
                //Mathf.Clamp (viewIndex, 1, deckList.listOfCards.Count);
                EditorGUILayout.LabelField("of   " + deckList.listOfCards.Count.ToString() + "  cards", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                deckList.listOfCards[viewIndex - 1].cardID = EditorGUILayout.IntField("Card ID", deckList.listOfCards[viewIndex - 1].cardID, GUILayout.ExpandWidth(false));
                
                GUILayout.Space(10);

                
                deckList.listOfCards[viewIndex - 1].cardName = EditorGUILayout.TextField("Card Name", deckList.listOfCards[viewIndex - 1].cardName as string);
                deckList.listOfCards[viewIndex - 1].cardImage = EditorGUILayout.ObjectField("Card Icon", deckList.listOfCards[viewIndex - 1].cardImage, typeof(Sprite), false) as Sprite;
                deckList.listOfCards[viewIndex - 1].targetObject = EditorGUILayout.ObjectField("Cards target Object", deckList.listOfCards[viewIndex - 1].targetObject, typeof(GameObject), false) as GameObject;
                

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Deck is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(deckList);
        }
    }

    void CreateNewlistOfCards()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        deckList = CreateDeck.Create();
        if (deckList)
        {
            deckList.listOfCards = new List<Card>();
            string relPath = AssetDatabase.GetAssetPath(deckList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenlistOfCards()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Deck", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            deckList = AssetDatabase.LoadAssetAtPath(relPath, typeof(Deck)) as Deck;
            if (deckList.listOfCards == null)
                deckList.listOfCards = new List<Card>();
            if (deckList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddCard()
    {
        Card newItem = new Card();
        newItem.cardName = "New Card";
        deckList.listOfCards.Add(newItem);
        viewIndex = deckList.listOfCards.Count;
    }
    void DeleteItem(int index)
    {
        deckList.listOfCards.RemoveAt(index);
    }
}
