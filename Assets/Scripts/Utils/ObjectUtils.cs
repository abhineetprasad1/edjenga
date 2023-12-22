using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectUtils
{

    public static GameObject CreateObject(Vector3 pos, string key)
    {
        return CreateObject(pos, Vector3.one, key);
    }

    public static GameObject CreateObject(Vector3 pos, Vector3 scale, string key)
    {
        return CreateObject(pos, Vector3.zero, scale, key);
    }

    public static GameObject CreateObject(Vector3 pos, Vector3 angle, Vector3 scale, string key)
    {
        var poolService = ServiceLocator.GetService<ObjectPool>();
        var go =  poolService.GetObject(Constants.PREFAB_FOLDER + key);
        go.transform.position = pos;
        go.transform.localScale = scale;
        go.transform.localEulerAngles = angle;
        var viewController = go.GetComponent<ViewController>();
        viewController.Link();
        viewController.Show();
        return go;
    }

    public static GameObject CreateGround(Vector3 pos)
    {
        return CreateObject(pos, Constants.GROUND);
    }

    public static GameObject CreateJBlock(Vector3 pos,Vector3 angle, JBlock block, Transform parent)
    {
        var key = Constants.JBLOCK_GLASS;
        switch (block.mastery)
        {
            case 0:
                key = Constants.JBLOCK_GLASS;
                break;
            case 1:
                key = Constants.JBLOCK_WOOD;
                break;
            case 2:
                key = Constants.JBLOCK_STONE;
                break;
            default:
                break;

        }

        var go = CreateObject(pos, angle, Vector3.one, key);
        var viewController = go.GetComponent<JBlockViewController>();
        viewController.AssignJBlockModel(block);
        viewController.EnablePhysics(false);
        viewController.transform.parent = parent;

        return go;
    }


    public static GradeLabelViewController CreateGradeLabel(Vector3 pos,Vector3 angle, string text, Transform parent)
    {
        var go = CreateObject(pos, angle, Vector3.one, Constants.GRADELABEL);
        var viewC = go.GetComponent<GradeLabelViewController>();
        viewC.SetLabel(text);

        viewC.transform.parent = parent;

        return viewC;
    }
}
