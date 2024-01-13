
namespace BotManagerWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            Entry.Logger.Info("Start");
            try
            {
                await Entry.BotMechanism.CompileSources();
                await app.RunAsync();
                Entry.Logger.Info("End");
            }
            catch (Exception ex)
            {
                Entry.Logger.Fatal(ex, "BotManager Abort");
                throw;
            }
            finally
            {
                Entry.Close();
            }
        }
    }
}
