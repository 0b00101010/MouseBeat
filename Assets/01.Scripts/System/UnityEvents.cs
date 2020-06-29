using System;
using UnityEngine.Events;

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class VoidEvent : UnityEvent { }

[Serializable]
public class SongEvent: UnityEvent<SongData> { }