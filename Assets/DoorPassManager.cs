using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage {
    public GameObject stage;
    public int stageLevel;
}

public class DoorPassManager : MonoBehaviour
{
    [SerializeField] LayerMask _playerMask;

    [SerializeField] Vector3 _centrePos;
    [SerializeField] float _centreSize;

    [SerializeField] Vector3 _entry;
    [SerializeField] Vector3 _entrySize;
    
    [SerializeField] Vector3 _exit;
    [SerializeField] Vector3 _exitSize;

    [SerializeField] Stage[] _stages;

    bool passedEntry, passedCenter, passedExit;

    int _currentStage = 0;

    private void Start() {
        for (int i = 0; i < _stages.Length; i++) {
            _stages[i].stage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        bool inEntry = Physics.CheckBox(_entry, _entrySize, Quaternion.identity, _playerMask.value);
        bool inExit = Physics.CheckBox(_exit, _exitSize, Quaternion.identity, _playerMask.value);
        bool inCenter = Physics.CheckSphere(_centrePos, _centreSize, _playerMask.value);

        if (inEntry) {
            passedEntry = true;
        }
        if (inExit) {
            passedExit = true;
        }

        if (inCenter) {
            passedCenter = true;
        }

        if (!inEntry && !inCenter) {
            passedEntry = false;
            passedCenter = false;
        }
        if (!inExit && !inCenter) {
            passedExit = false;
            passedCenter = false;
        }

        if (inExit && passedCenter && passedEntry) {
            Debug.Log("Entry to exit");
            passedEntry = false;
            NextStage();
        }
        if (inEntry && passedCenter && passedExit) {
            Debug.Log("Exit to entry");
            passedExit = false;
            NextStage();
        }
    }

    void NextStage() {
        passedCenter = false;
        _currentStage++;

        for (int i = 0; i < _stages.Length; i++) {
            _stages[i].stage.SetActive(_stages[i].stageLevel == _currentStage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(_centrePos, _centreSize);
        Gizmos.DrawCube(_entry, _entrySize);
        Gizmos.DrawCube(_exit, _exitSize);
    }
}
