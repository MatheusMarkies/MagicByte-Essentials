using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics.Sound
{
    public class Spectrum : MonoBehaviour
    {
        public GameObject[] createSpectrum(int index, float size, GameObject SpectrumPrefab, float elementOffset)
        {
            GameObject[] elementsArray = new GameObject[index];
            for (int i = 0; i < index; i++)
            {
                elementsArray[i] = Instantiate(SpectrumPrefab, new Vector3(0, 0, 5), Quaternion.identity);
                elementsArray[i].name = "Element " + i;
                float objectSize = elementsArray[i].transform.localScale.x;
                elementsArray[i].transform.position = new Vector3(-size / 2 + (size / (index) / objectSize + elementOffset) * i, 0, elementsArray[i].transform.position.z);
            }
            return elementsArray;
        }

        public void updateSpectrum(GameObject[] elements, float[] samples, float spectrumSize)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i].transform.localScale = new Vector3(elements[i].transform.localScale.x, samples[i] * spectrumSize, elements[i].transform.localScale.z);
            }
        }

    }
}
