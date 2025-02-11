using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class HogeSwordTracker : MonoBehaviour, ISwordTracker
{
     private List<string> FoodChildrenName = new();
    [SerializeField] GameObject[] FoodParentPoint;
    [SerializeField] FoodObjectListSO foodObjectListSO;
    [SerializeField] FoodRecipeListSO foodRecipeListSO;

    private int FoodCounter = 0;
    private const int MaxFoodCounter = 3;
    private readonly Subject<Unit> _swordFullStabbEvent = new();
    public IObservable<Unit> SwordFullStabbEvent => _swordFullStabbEvent;

    public void Start()
    {
        FoodCounter = 0;
    }
    public void OnStabbed(string name, GameObject foodObj)
    {
        FoodChildrenName.Add(name);
        Debug.Log(FoodCounter);
        foodObj.transform.parent = FoodParentPoint[FoodCounter].transform;
        //foodObj.transform.SetParent(FoodParentPoint[FoodCounter].transform, false);
        foodObj.transform.position = FoodParentPoint[FoodCounter].transform.position;

        //�傫���𐳋K������
        var localScale = foodObj.transform.localScale;
        var parentLossyScale = FoodParentPoint[FoodCounter].transform.localScale;
        Debug.Log($"{new Vector3(parentLossyScale.x, parentLossyScale.y, parentLossyScale.z)}����");
        //Debug.Log($"{new Vector3(parentLossyScale.x, parentLossyScale.y, parentLossyScale.z)}����");
       // foodObj.transform.localScale
          //  = new Vector3(localScale.x / parentLossyScale.x, localScale.y / parentLossyScale.y, localScale.z / parentLossyScale.z);

        FoodCounter++;
        if (FoodCounter == MaxFoodCounter)
        {
            Debug.Log("Full");
            List<FoodDataBaseSO> recipe = new();
            foreach (var foodname in FoodChildrenName)
            {
                recipe.Add(foodObjectListSO.GetFoodData(foodname));
            }
            var hitrecipe = foodRecipeListSO.GetRecipefromFoodData(recipe);
            Debug.Log(hitrecipe.FoodRecipeName);
            _swordFullStabbEvent.OnNext(Unit.Default);
            TestDestroy(this.transform);
        }

    }


    // root�̎q�I�u�W�F�N�g�����ׂ�Destroy����
    public void TestDestroy(Transform root)
    {
        //�����̎q����S�Ē��ׂ�
        foreach (Transform child in root)
        {
            //�����̎q����Destroy����
            Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }
}
