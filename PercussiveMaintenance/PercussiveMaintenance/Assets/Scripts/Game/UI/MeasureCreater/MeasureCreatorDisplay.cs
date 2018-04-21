using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureCreatorDisplay : MonoBehaviour
{
    public List<TowerSoundSource> TowerSoundSources;
    public GameObject LinePrefab;
    public GameObject LineContainer;

    public BeatKeeper BeatKeeper;
	// Use this for initialization
	void Start ()
    {
	    foreach(var tower in TowerSoundSources)
        {
            var newLine = Instantiate(LinePrefab);
            newLine.transform.SetParent(LineContainer.transform);
            MeasureCreatorTowerLine towerLine = newLine.GetComponent<MeasureCreatorTowerLine>();
            towerLine.SetTowerSoundSource(tower);
        }
        InitToggles();
        UpdateBeatKeeper();
	}

    public void InitToggles()
    {
        for (int i = 0; i < LineContainer.transform.childCount; i++)
        {
            var child = LineContainer.transform.GetChild(i);
            MeasureCreatorTowerLine line = child.GetComponent<MeasureCreatorTowerLine>();
            var tower = TowerSoundSources[i];
            line.SetToggles(tower.MeasurePlayTimes);
        }
    }

    public void UpdateTowerSoundSources()
    {
        for(int i = 0; i < LineContainer.transform.childCount; i++)
        {
            var child = LineContainer.transform.GetChild(i);
            MeasureCreatorTowerLine line = child.GetComponent<MeasureCreatorTowerLine>();
            var tower = TowerSoundSources[i];
            tower.MeasurePlayTimes = line.GetMeasuresArray();
        }
    }

    public void UpdateBeatKeeper()
    {
        BeatKeeper.TowerSources = TowerSoundSources;
    }

    public void OnPlayPausePressed()
    {
        UpdateTowerSoundSources();
        UpdateBeatKeeper();
        BeatKeeper.TogglePlayStop();
    }
	
}
