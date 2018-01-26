using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Error
{
    public static void NI(string class_name, int line_number)
    {
        Debug.Log("-" + class_name + "- line " + line_number + " - not implemented");
    }
}
