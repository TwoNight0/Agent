using UnityEngine.Events;

public class cPopup {
    public cPopup(string _tile, string _value, UnityAction _action){
        Title = _tile;
        Value = _value;
        Action = _action;
    }
    public string Title;
    public string Value;
    public UnityAction Action;
}
