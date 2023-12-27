using System;

namespace TwoWindowsMVVM.Model;

internal class TextMessageModel
{
    public DateTime Time { get; }
    public string Text { get; }

    public TextMessageModel(DateTime time, string message) => (Time, Text) = (time, message);
    
    public TextMessageModel(string message) : this(DateTime.Now, message) { }

}