using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureCreatorTowerLine : MonoBehaviour
{
    private TowerSoundSource TowerSoundSource;
    public Text LabelText;

    public GameObject MeasureBox;

    public List<string> GetMeasuresArray()
    {
        var outList = new List<string>();

        //Skip the label
        for(int i = 1; i < MeasureBox.transform.childCount; i++)
        {
            outList.Add(GetMeasureString(i));
        }

        return outList;
    }

    public string GetMeasureString(int measureNumber)
    {
        var outString = ""; 
        var box = MeasureBox.transform.GetChild(measureNumber);
        for(int i = 0; i < box.childCount; i++)
        {
            var toggleComp = box.GetChild(i);
            Toggle toggle = toggleComp.GetComponent<Toggle>();
            outString = toggle.isOn ?
               outString + "1" :
               outString + "0";
        }
        return outString;
    }

    public GameObject GetMeasureContainer(int measureNumber)
    {
        return MeasureBox.transform.GetChild(measureNumber).gameObject;
    }

    public void SetTowerSoundSource(TowerSoundSource source)
    {
        LabelText.text = source.gameObject.name;
        TowerSoundSource = source;
    }

    internal void SetToggles(List<string> measurePlayTimes)
    {
        for(int i = 0; i < measurePlayTimes.Count; i++)
        {
            var container = GetMeasureContainer(i);
            for(int j = 0; j < container.transform.childCount; j++)
            {
                var child = container.transform.GetChild(j);
                var toggle = child.GetComponent<Toggle>();
                toggle.isOn = measurePlayTimes[i][j] == '1';
            }
        }
    }
}
