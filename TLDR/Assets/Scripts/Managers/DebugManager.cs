using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    
    public int dmgDir
    {
        get { return dmgDir; }
        set
        {
            if (dmgDir == value) return;
            dmgDir = value;

            if (OnVariableChange != null)
                OnVariableChange(dmgDir, true, BulletManager.BulletType.Direct);
        }
    }

    public int dmgCon
    {
        get { return dmgCon; }
        set
        {
            if (dmgCon == value) return;
            dmgCon = value;

            if (OnVariableChange != null)
                OnVariableChange(dmgCon, true, BulletManager.BulletType.Cone);
        }
    }

    public int dmgAOE
    {
        get { return dmgAOE; }
        set 
        {
            if (dmgAOE == value) return;
            dmgAOE = value;

            if (OnVariableChange != null)
                OnVariableChange(dmgAOE, true, BulletManager.BulletType.AOE);
        }
    }

    public int spdDir
    {
        get { return spdDir; }
        set
        {
            if (spdDir == value) return;
            spdDir = value;

            if (OnVariableChange != null)
                OnVariableChange(spdDir, false, BulletManager.BulletType.Direct);
        }
    }

    public int spdCon
    {
        get { return spdCon; }
        set
        {
            if (spdCon == value) return;
            spdCon = value;


            if (OnVariableChange != null)
                OnVariableChange(spdCon, false, BulletManager.BulletType.Cone);
        }
    }

    public int spdAOE
    {
        get { return spdAOE; }
        set
        {
            if (spdAOE == value) return;
            spdAOE = value;


            if (OnVariableChange != null)
                OnVariableChange(spdAOE, false, BulletManager.BulletType.AOE);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public delegate void OnVariableChangeDelegate(int newVal, bool isDmg, BulletManager.BulletType type);

    public event OnVariableChangeDelegate OnVariableChange;

    /// <summary>
    /// Set the value of the bullet.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isDmg"></param>
    /// <param name="type"></param>
    public void SetValue(float value, bool isDmg, BulletManager.BulletType type)
    {
        Debug.Log("Setting value: " + value + " isDmg: " + isDmg + " BulletType: " + type.ToString());
    }
}
