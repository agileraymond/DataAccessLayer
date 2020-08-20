using System.Collections.Generic;
using System;
using KeonaDataLayer.Models;

public interface IDataAccessLayer
{
    IList<Person> GetPeopleWithOrders(DateTime orderDate);
}