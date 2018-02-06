using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public static class Function
{
    public static int FindImageID(List<Sprite> images, string imageToFind)
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (images[i].name == imageToFind)
            {
                return i;
            }
        }

        return 0;
    }
}
