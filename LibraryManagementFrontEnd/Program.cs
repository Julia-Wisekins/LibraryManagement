using LibraryManagementFrontEnd.Components;
using LibraryManagementBackend.Testing;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddServerSideBlazor();



        builder.Services.AddSingleton(BookData => LibraryManagementServiceTest.BookRepository());

        builder.Services.AddSingleton(ReaderData => LibraryManagementServiceTest.ReaderResository());


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
    }
}