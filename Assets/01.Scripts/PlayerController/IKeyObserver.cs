using UnityEngine;

public interface IKeyObserver{
    void KeyUpNotify(KeyCode key);
    void KeyHoldingNotify(KeyCode key);
    void KeyDownNotify(KeyCode key);
}