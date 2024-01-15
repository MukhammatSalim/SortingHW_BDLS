using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public int NumberOfObjects;
    public GameObject WhatToSpawn;
    public Transform ParentForObjects;
    [SerializeField] private List<GameObject> SortingCubes; 
    [SerializeField] private bool DebuggingMode;
    // Start is called before the first frame update
    void Start()
    {
        ResetCubes();
    }

    public void SpawnObject(GameObject spawnable, Vector3 distance, Transform parent){
        GameObject SpawnedObject = Instantiate(spawnable,parent);
        SpawnedObject.transform.localScale += new Vector3 (0,Random.Range(0.1f,5f),0);
        SpawnedObject.transform.position = distance;
        SortingCubes.Add(SpawnedObject);
    }

    public void ResetCubes(){
        for (int i = 0; i< SortingCubes.Count;i++){
            Destroy(SortingCubes[i]);
        }
        SortingCubes.Clear();
        for (int i = 0; i< NumberOfObjects;i++ ){
            Vector3 DistanceBetweenColumns = new Vector3(i+2,0,0); 
            SpawnObject(WhatToSpawn,DistanceBetweenColumns,ParentForObjects);
        }
        if (DebuggingMode){Debug.Log("Cubes Reseted");}
    }

    public void Swap(int first, int second){
        Vector3 temp = new Vector3 (1,SortingCubes[first].transform.localScale.y,1);
        SortingCubes[first].transform.localScale = new Vector3 (1,SortingCubes[second].transform.localScale.y,1);
        SortingCubes[second].transform.localScale = temp;
    }

    public void DoBubbleSort(){
        for (int i = 0; i< SortingCubes.Count -1 ; i++){
            for (int j = 0; j < SortingCubes.Count - i - 1 ; j++){
                if (SortingCubes[j].transform.localScale.y > SortingCubes[j+1].transform.localScale.y){
                    Swap(j, j+1);
                }
            }
        }
        if (DebuggingMode){Debug.Log("Completed Bubble Sort");}
    }

    public void DoInsertionSort(){
        for (int i = 0; i< SortingCubes.Count - 1; i++){
            for (int j = i + 1; j>0 ; j--){
                if (SortingCubes[j-1].transform.localScale.y > SortingCubes[j].transform.localScale.y){
                    Swap(j-1, j);
                }
            }
        }
        if (DebuggingMode){Debug.Log("Completed Insertion Sort");}
    }

    public void DoSelectionSort(){
        for (int i = 0; i< SortingCubes.Count -1 ; i++){
            int smallest = i;
            for (int j = i+1; j < SortingCubes.Count; j++){
                if (SortingCubes[j].transform.localScale.y < SortingCubes[smallest].transform.localScale.y){
                    Swap(j, smallest);
                }
            }
        }
        if (DebuggingMode){Debug.Log("Completed Selection Sort");}
    }

    public void DoQuickSort(){
        Quick_sort(0,SortingCubes.Count-1);
        if (DebuggingMode){Debug.Log("Completed Quick Sort");}
    }
    
    private void Quick_sort(int left, int right){
        if (left < right)
        {
            int pivot = Partition(left, right);
            
            if (pivot > 1){
                Quick_sort(left, pivot - 1);
            }
            if (pivot + 1 < right){
                Quick_sort(pivot + 1, right);
            }
        }
    }

    private int Partition(int left,int right){
            GameObject pivot = SortingCubes[left];
            while (true){
                
                while (SortingCubes[left].transform.localScale.y < pivot.transform.localScale.y)
                {
                    left++;
                }

                while (SortingCubes[right].transform.localScale.y > pivot.transform.localScale.y)
                {
                    right--;
                }

                if (left < right)
                {
                    if (SortingCubes[left] == SortingCubes[right]) return right; 
                    Swap(left, right);
                }
                else
                {
                    return right;
                }
        }
    }
}
