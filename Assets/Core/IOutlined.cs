using System;

public interface IOutlined
{
    Action OnInteractEnded { get; set; }
    void TurnOnOutline();
}
