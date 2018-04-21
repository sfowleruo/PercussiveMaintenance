using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;


public class InputManager : Manager
{
    public Sprite CursorSprite;
    public static InputManager Inst;
    public Tile TileUnderMouse { get { return GetTileUnderMouse(); } }
    public System.Action<Tile> OnLeftMouseDown;
    public System.Action<Tile> OnLeftMouse;
    public System.Action<Tile> OnLeftMouseUp;

    GameObject Selection;
    TileGridManager TileManager;
    Vector3 currentMousePos;

    public override void Init()
    {
        Inst = this;

        Cursor.SetCursor(CursorSprite.texture, Vector2.zero, CursorMode.Auto);
        TileManager = TileGridManager.Inst;
        var tileSize = TileGridManager.Inst.TileSize;
        Status = ManagerStatus.Loaded;
    }

    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Ground")))
        {
            currentMousePos = hit.point;
            currentMousePos.y = 0;
        }

        UpdateMouseInput();

    }

    Tile GetTileUnderMouse()
    {
        return TileGridManager.Inst.GetTileAt(currentMousePos);
    }

    void UpdateMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (OnLeftMouseDown != null)
                OnLeftMouseDown(TileUnderMouse);
        }

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (OnLeftMouse != null)
                OnLeftMouse(TileUnderMouse);
        }

        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (OnLeftMouseUp != null)
                OnLeftMouseUp(TileUnderMouse);
        }
    }

}


