using System.Collections;

using UnityEngine;

public class DemoIEnumerator : MonoBehaviour
{
    [SerializeField] private int[] _myTab = { 1, 2, 3, 4 };

    private void Start()
    {
        for (int i = 0; i < _myTab.Length; i++)
        {
            Debug.Log(_myTab[i]);
        }

        foreach (int element in _myTab)
        {
            Debug.Log(element);
        }

        MyEnumerator myEnum = new MyEnumerator(_myTab);

        //IEnumerator enumerator = myEnum.GetEnumerator();
        myEnum.Reset();
        while (myEnum.MoveNext())
        {
            Debug.Log(myEnum.Current);
        }

        foreach (int element in myEnum) // utilise la fonction GetEnumerator()
        {
            Debug.Log(element);
        }
    }
}


public class MyEnumerator : IEnumerator
{
    #region IEnumerator interface

    public object Current
    {
        get
        {
            return _tab[_index];
        }
    }

    public bool MoveNext()
    {
        _index++;
        return _index < _tab.Length;
    }

    public void Reset()
    {
        _index = -1;
    }
    #endregion


    #region ForEach

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    #endregion


    public MyEnumerator(int[] tab)
    {
        _tab = tab;
        _index = -1;
    }

    private int[] _tab;
    private int _index;
}