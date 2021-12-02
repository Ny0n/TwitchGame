using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DemoIEnumerable : MonoBehaviour
{
    [SerializeField] private int[] _myTab = { 1, 2, 3, 4 };

    private void Start()
    {
        foreach (int i in new MyEnumerable(_myTab))
        {
            Debug.Log(i);
        }

        foreach (int i in MyInts)
        {
            Debug.Log(i);
        }

        foreach (object obj in MyObjects())
        {
            Debug.Log(obj);
        }

        foreach (int power in Powers(2, 8))
        {
            Debug.Log(power);
        }
    }

    public class MyEnumerable : IEnumerable
    {
        private int[] _tab;

        public MyEnumerable(int[] tab)
        {
            _tab = tab;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(_tab);
        }
    }

    public IEnumerable<int> MyInts
    {
        get
        {
            yield return 1;
            yield return 3;
            yield return 5;
            yield return 7;
            yield return 9;
        }
    }

    public IEnumerable MyObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return i;
            yield return $"toto {i}";
        }
    }

    public IEnumerable<int> Powers(int number, int exponent)
    {
        int result = 1;

        for (int i = 0; i < exponent; i++)
        {
            result = result * number;
            yield return result;
        }
    }
}
