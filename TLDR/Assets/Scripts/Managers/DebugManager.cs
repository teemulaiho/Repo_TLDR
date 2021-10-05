using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    int dmgDir = 0;
    int dmgCon = 0;
    int dmgAOE = 0;
    int spdDir = 0;
    int spdCon = 0;
    int spdAOE = 0;
    
    public int DmgDir
    {
        get { return dmgDir; }
        set
        {
            if (dmgDir == value) return;
            dmgDir = value;

            Debug.Log("DmgDir value changed to " + dmgDir);

            if (OnVariableChange != null)
                OnVariableChange(dmgDir, true, PlayerManager.PlayerStructures.TowerDirect);
            //OnVariableChange(dmgDir, true, BulletManager.BulletType.Direct);
        }
    }

    public int DmgCon
    {
        get { return dmgCon; }
        set
        {
            if (dmgCon == value) return;
            dmgCon = value;

            if (OnVariableChange != null)
                OnVariableChange(dmgCon, true, PlayerManager.PlayerStructures.TowerCone);
                //OnVariableChange(dmgCon, true, BulletManager.BulletType.Cone);
        }
    }

    public int DmgAOE
    {
        get { return dmgAOE; }
        set 
        {
            if (dmgAOE == value) return;
            dmgAOE = value;

            if (OnVariableChange != null)
                OnVariableChange(dmgAOE, true, PlayerManager.PlayerStructures.TowerAOE);
                //OnVariableChange(dmgAOE, true, BulletManager.BulletType.AOE);
        }
    }

    public int SpdDir
    {
        get { return spdDir; }
        set
        {
            if (spdDir == value) return;
            spdDir = value;

            if (OnVariableChange != null)
                OnVariableChange(spdDir, false, PlayerManager.PlayerStructures.TowerDirect);
                //OnVariableChange(spdDir, false, BulletManager.BulletType.Direct);
        }
    }

    public int SpdCon
    {
        get { return spdCon; }
        set
        {
            if (spdCon == value) return;
            spdCon = value;


            if (OnVariableChange != null)
                OnVariableChange(spdCon, false, PlayerManager.PlayerStructures.TowerCone);
                //OnVariableChange(spdCon, false, BulletManager.BulletType.Cone);
        }
    }

    public int SpdAOE
    {
        get { return spdAOE; }
        set
        {
            if (spdAOE == value) return;
            spdAOE = value;


            if (OnVariableChange != null)
                OnVariableChange(spdAOE, false, PlayerManager.PlayerStructures.TowerAOE);
                //OnVariableChange(spdAOE, false, BulletManager.BulletType.AOE);
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

    public delegate void OnVariableChangeDelegate(int newVal, bool isDmg, PlayerManager.PlayerStructures type);

    public event OnVariableChangeDelegate OnVariableChange;

    /// <summary>
    /// Set the value of the bullet.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isDmg"></param>
    /// <param name="type"></param>
    public void SetValue(int value, bool isDmg, BulletManager.BulletType type)
    {
        if (isDmg)
        {
            if (type == BulletManager.BulletType.Direct)
            {
                if (dmgDir != value)
                {
                    //dmgDir = value;
                    DmgDir = value;
                }
            }
            else if (type == BulletManager.BulletType.Cone)
            {
                //dmgCon = value;
                DmgCon = value;
            }
            else if (type == BulletManager.BulletType.AOE)
            {
                //dmgAOE = value;
                DmgAOE = value;
            }
        }
        else
        {
            if (type == BulletManager.BulletType.Direct)
            {
                //spdDir = value;
                SpdDir = value;
            }
            else if (type == BulletManager.BulletType.Cone)
            {
                //spdCon = value;
                SpdCon = value;
            }
            else if (type == BulletManager.BulletType.AOE)
            {
                //spdAOE = value;
                SpdAOE = value;
            }
        }
    }
}
