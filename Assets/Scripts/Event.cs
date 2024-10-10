using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Event : MonoBehaviour
{
    private Event_List eventList;
    private List<MethodInfo> methods;


    private void Start()
    {
        eventList = FindFirstObjectByType<Event_List>();
        methods = eventList.GetType().GetMethods(BindingFlags.DeclaredOnly).ToList();
        foreach (MethodInfo method in methods)
        {
            print(method);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        eventList.Cube();
    }
}
