using System;

public interface IRepairable
{   
    Action OnRepairEnded { get; set; }
    void EndRepairing();
}
