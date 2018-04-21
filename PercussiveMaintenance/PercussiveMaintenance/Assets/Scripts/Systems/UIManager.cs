using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager
{
    public Material PrototypeMat;
    GameObject BuildMenu;
    GameObject TowerMenu;
    Button OpenTowerMenu;

    InputManager Input;
    GameObject CurrentActivePrototype;


    public override void Init()
    {
        Input = InputManager.Inst;
        Input.OnLeftMouseUp = SelectObject;
        InitUI();


        Status = ManagerStatus.Loaded;
    }

    void InitUI()
    {
        BuildMenu = GameObject.Find("BuildMenu");
        TowerMenu = BuildMenu.transform.Find("TowerMenu").gameObject;
        OpenTowerMenu = BuildMenu.transform.Find("OpenTowerMenu").GetComponent<Button>();

        TowerMenu.SetActive(false);
        OpenTowerMenu.onClick.AddListener(ToggleTowerMenu);

    }

    private void Update()
    {
        if (CurrentActivePrototype != null)
        {
            var currentTile = Input.TileUnderMouse;
            CurrentActivePrototype.transform.position = currentTile.GetWorldPos();
            if (currentTile.Object != null)
                CurrentActivePrototype.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, .1f);
            else
                CurrentActivePrototype.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, .1f);
        }
    }

    public void CreatePrototype(string name)
    {
        ResetUI();
        Cursor.visible = false;

        var tower = ResourceManager.Inst.GetNewTower(name);
        CurrentActivePrototype = tower.gameObject;
        CurrentActivePrototype.layer = 2;//Ignore Raycast layer
        var meshRend = CurrentActivePrototype.GetComponent<MeshRenderer>();
        meshRend.material = PrototypeMat;
        meshRend.material.color = new Color(0, 1, 0, .1f);
        Input.OnLeftMouseUp = PlaceTower;
    }

    public void ToggleTowerMenu()
    {
        ResetUI();

        TowerMenu.SetActive(!TowerMenu.activeSelf);
    }

    void PlaceTower(Tile tile)
    {
        if (tile.Object != null)
        {
            Debug.LogError("Tile Occupado");
            return;
        }
        var tower = ResourceManager.Inst.GetNewTower(CurrentActivePrototype.name);
        tower.transform.position = tile.GetWorldPos();
        tile.Object = tower.gameObject;

        ResetUI();
    }

    void SelectObject(Tile tile)
    {
        //do we need this
    }

    void ResetUI()
    {
        if (CurrentActivePrototype != null)
            Destroy(CurrentActivePrototype);

        Cursor.visible = true;
    }
}
