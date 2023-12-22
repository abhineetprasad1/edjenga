using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBlockViewController : ViewController
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Renderer blockRenderer;
    [SerializeField] GameObject litBlock;

    private JBlock _jBlock;
    public JBlock jBlock
    {
        get { return _jBlock; }
        set {
            _jBlock = value;

        }
    }




    private bool _isLit = false;
    public void AssignJBlockModel(JBlock jBlock)
    {
        _jBlock = jBlock;
    }

    public override void Show()
    {
        base.Show();
        litBlock.SetActive(false);
    }

    public void EnablePhysics(bool shouldEnable)
    {
        rigidbody.useGravity = shouldEnable;
        rigidbody.isKinematic = !shouldEnable;
    }

    public void SetHighlight(bool status)
    {
        if (_isLit == status)
            return;

        _isLit = status;

        litBlock.SetActive(_isLit);
    }

}
