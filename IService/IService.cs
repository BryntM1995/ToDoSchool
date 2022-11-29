using School.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace School.IService
{
    internal interface IService<S>
    {
        void AddToRecord(S value);
        bool RemoveFromRecord(int value);
        void UpdateFromRecord(S value);
        List<S> GetAllRecords();
        S GetRecord(int key);
    }
}
