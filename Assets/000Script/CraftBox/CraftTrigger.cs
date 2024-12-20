using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CraftTrigger : MonoBehaviour
{
    public Sprite craftButtonImage;
    [SerializeField] Button _actionButton;
    [SerializeField] GameObject _craftPanelPrefab;
    [SerializeField] MenuPanelManager _menuPanelManager;

    //public GameObject _craftPanelInstance;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _actionButton.gameObject.SetActive(true);
            _actionButton.image.sprite = craftButtonImage;
            Player player = other.gameObject.GetComponent<Player>();
            _actionButton.onClick.AddListener(() => OpenCraftPanel(player));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _actionButton.gameObject.SetActive(false);
            _actionButton.image.sprite = null;
            Player player = other.gameObject.GetComponent<Player>();
            _actionButton.onClick.RemoveListener(() => OpenCraftPanel(player));
        }
    }


    void OpenCraftPanel(Player player)
    {
        _menuPanelManager.InstiateManuPanel(_craftPanelPrefab);
        gameObject.GetComponent<RecipeRenderer>().PasteRecipe(_menuPanelManager.getManuPanelInstance(), player);
    }

}
