
using BankPassword.Repositories;
using BankPassword.Repositories.Connections;
using BankPassword.Services;

namespace BankPassword {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IRedisConnectionFactory>(f => new RedisConnectionFactory(builder.Configuration));
            builder.Services.AddScoped<IRedisRepository, RedisRepository>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}