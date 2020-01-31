﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventManager 
{
    void DoRandomEvent();
}

public interface IEvent
{
    bool CanBeDone();
    void DoEvent();
    bool done { get; }
    string eventText { get; }
}