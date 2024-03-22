using KubeOps.Operator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddKubernetesOperator();
builder.Services.AddControllers();

var app = builder.Build();
app.UseKubernetesOperator();

app.MapControllerRoute(
       name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

if (!args.Contains("--run-only"))
{
    await app.RunOperatorAsync(args);
}
else
{
    app.RunOperatorAsync(args);
    app.Run();
}

