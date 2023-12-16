using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.
app.UseFrom20thousandTo100thousand();
app.UseFrom11thousendTo20thousand();
app.UseFrom1thousandTo10thousand();
app.UseFrom100To1000();
app.UseFromTwentyToHundred();
app.UseFromElevenToNineteen();
app.UseFromOneToTen();

app.Run();
