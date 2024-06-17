using MongoDB.Bson;
using System;

public class DataBaseOverdue
{
    public ObjectId Id { get; set; }
    public string _title { get; set; }
    public double _price { get; set; }
    public string _userId { get; set; }
    public string _bookedDate { get; set; }
    public string _deadlineDate { get; set; }
    public string _paymentMethod { get; set; }
    public bool _isAdmin { get; set; }
    public string _status { get; set; }
}
