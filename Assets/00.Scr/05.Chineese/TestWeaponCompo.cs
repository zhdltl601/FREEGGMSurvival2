using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponCompo : WeaponCompo
{
    [SerializeField] private GameObject wallPrefab, groundPrefab;

    [SerializeField] private BuildingSystem buildingSystemCompo;
    //

    private int currentBuildingIdx = 0;


    public override void Fire(int bulletIndex)
    {
        base.Fire(bulletIndex);
        Debug.Log("입력 마우스 " + bulletIndex);
        if(bulletIndex == 0)
        {
            buildingSystemCompo.InputLeftMouseButton();
        }
        if(bulletIndex == 1)
        {
            if(currentBuildingIdx > 1)
            {
                currentBuildingIdx = 0;
            }
            buildingSystemCompo.InputRightMouseButton(currentBuildingIdx);
            currentBuildingIdx++;
        }
    } 
    public override void TestETSe()
    {
        //
    }
}
