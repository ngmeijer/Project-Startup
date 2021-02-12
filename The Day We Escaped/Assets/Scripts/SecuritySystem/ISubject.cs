﻿using System.Collections.Generic;

public interface ISubject
{
    List<IObserver> _observerList { get; }

    EnemyAlertLevel AlertLevel { get; }
    int SubjectState { get; }

    void Attach(IObserver pObserver);
    void Detach(IObserver pObserver);
    void NotifyObservers();
}
