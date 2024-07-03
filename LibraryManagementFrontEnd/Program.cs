using LibraryManagementFrontEnd.Components;
using LibraryManagementBackend.Interface;
using LibraryManagementBackend;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();



builder.Services.AddSingleton(BookData =>LibraryManagementService.BookRepository());

builder.Services.AddSingleton(ReaderData => LibraryManagementService.ReaderResository());

builder.Services.AddSingleton(ReaderData => LibraryManagementService.ReaderResository());


//builder.Services.AddSingleton<NavigationManagerService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
