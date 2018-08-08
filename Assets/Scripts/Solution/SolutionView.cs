using System;
using DG.Tweening;
using Solution.Data;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Solution
{
    public class SolutionView : MonoBehaviour
    {
        [SerializeField] private GameObject _ringPrefab;
        [SerializeField] private Transform[] _palki;

        private Action<int, int, int> _onCompleteMove;
        private float _speed;
        private GameObject[] _ringsPic;

        public void Listen(Action<int, int, int> onCompleteMove)
        {
            _onCompleteMove = onCompleteMove;
        }

        public void Unlisten()
        {
            _onCompleteMove = null;
        }

        public void DestroyRings()
        {
            for (int i = 0; i < _ringsPic.Length; i++)
            {
                Destroy(_ringsPic[i]);
            }            
        }
        
        public void Show(bool show)
        {         
            gameObject.SetActive(show);            
        }

        public void CreateRings(Ring[] _rings)
        {
            _ringsPic = new GameObject[_rings.Length];
            for (int i = 0; i < _ringsPic.Length; i++)
            {
                var size = 1 - 0.6f / _ringsPic.Length * i;                
                
                _ringsPic[i] = Instantiate(_ringPrefab, transform, false);
                _ringsPic[i].transform.position = _palki[_rings[i].Place].position + new Vector3(0, GetPosition(_rings[i].Position), 0);
                _ringsPic[i].transform.localScale = new Vector3(size, 0.75f, size);
                _ringsPic[i].GetComponent<Image>().color = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));                
            }            
        }

        private float GetPosition(int position)
        {
            return 29 * position - 117;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void Move(int index, int toPlace, int toPosition)
        {
            _ringsPic[index].transform.DOLocalMoveY(245, 5 / _speed).OnComplete(() =>
            {
                _ringsPic[index].transform.DOLocalMoveX(_palki[toPlace].localPosition.x, 5 / _speed).OnComplete(() =>
                    {
                        _ringsPic[index].transform.DOMoveY(_palki[toPlace].position.y +  GetPosition(toPosition), 5 / _speed).OnComplete(() =>
                        {
                            if (_onCompleteMove != null) _onCompleteMove.Invoke(index, toPlace, toPosition);
                        });
                    });                
            });
        }

    }
}