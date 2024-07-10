using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoMove : MonoBehaviour {
    [Header("Rotate")]
    [SerializeField] Vector3 _rotate;
    [SerializeField] bool _randomRotate;
    [SerializeField] float _rotateSpeed = 1;
    [SerializeField] bool _randomRotSpeed;
    [SerializeField] Vector2 _rotSpeedRange;

    [SerializeField] bool _lookAt;
    [SerializeField] Transform _lookAtTarget;

    [Header("Lerp")]
    [SerializeField] Vector3 _lerpFrom;
    [SerializeField] Vector3 _lerpTo;
    [SerializeField] float _lerpSpeed;

    [SerializeField] bool _randomSpeed;
    [SerializeField] Vector2 _speedRange;

    [Header("Scale")]
    [SerializeField] bool _randomScale;
    [SerializeField] Vector3 _scaleFrom;
    [SerializeField] Vector3 _scaleTo;

    Vector3 _startPos;


    float t = 0;

    // Start is called before the first frame update
    void Start() {
        _startPos = transform.position;

        if (_randomRotate) {
            _rotate = Random.insideUnitSphere;
            _rotateSpeed = Random.Range(0.5f, 1f);
        }

        if (_randomSpeed) {
            _lerpSpeed = Random.Range(_speedRange.x, _speedRange.y);
        }
        if(_randomRotSpeed) {
            _rotateSpeed = Random.Range(_rotSpeedRange.x, _rotSpeedRange.y);
        }

        if (_randomScale) {
            transform.localScale = Vector3.Lerp(_scaleFrom, _scaleTo, Random.value);
        }
    }

    // Update is called once per frame
    void Update() {
        if (_lookAt) {
            transform.LookAt(_lookAtTarget);
        } else {
            transform.rotation *= Quaternion.Euler(_rotate * _rotateSpeed);
        }

        t += Time.deltaTime;
        transform.position = Vector3.Lerp(_startPos + _lerpFrom, _startPos + _lerpTo, (Mathf.Sin(t * _lerpSpeed) + 1f) * 0.5f);
    }

    private void OnValidate() {
        _startPos = transform.position;
    }

    private void OnDrawGizmos() {
        //_startPos = transform.position;
        Gizmos.DrawSphere(_startPos + _lerpFrom, 0.1f);
        Gizmos.DrawSphere(_startPos + _lerpTo, 0.1f);
    }
}
