using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddCascadingValue(s => new CascadingValueSource<User>(new User(), false));

await builder.Build().RunAsync();

public class User
{
    public int Clicks { get; set; }
}