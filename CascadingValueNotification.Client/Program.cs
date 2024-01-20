using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddCascadingValue(s => new CascadingValueSource<User>(new User(), false));

// this one will notify when the value changes, see NotifyingCounter.razor
builder.Services.AddCascadingValue(s => CascadingValueSource.CreateNotifying(new NotifyingUser()));

await builder.Build().RunAsync();

public class User
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public int Clicks { get; set; }
}


public class NotifyingUser : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public string Id { get; } = Guid.NewGuid().ToString();

    int clicks;
    public int Clicks
    {
        get => clicks;
        set
        {
            if (clicks != value)
            {
                clicks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Clicks)));
            }
        }
    }
}

public static class CascadingValueSource
{
    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false) where T : INotifyPropertyChanged
    {
        var source = new CascadingValueSource<T>(value, isFixed);

        value.PropertyChanged += (sender, args) => source.NotifyChangedAsync();

        return source;
    }
}