using DBMapping.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Why AddNewtonsoftjson was added
/*
 * An error accuring possible object cycle was detected
 * Understanding the Error:

This error occurs when you try to serialize an object in ASP.NET that has circular references. Circular references happen when two or more objects hold references to each other, creating an infinite loop during serialization. Serialization is the process of converting an object into a format (often JSON) that can be transmitted or stored.

Common Scenarios:

Entity Framework Relationships: This is a frequent cause, especially in one-to-many or many-to-many relationships between entities. Imagine an Order object with a collection of Product objects, and each Product might have a reference back to the Order it belongs to.
Custom Object Structures: If you have custom objects with references to each other in a circular manner, you can encounter this error as well.
Resolving the Error:

There are two main approaches to fix this error:

Refactoring Object Structure:

Break Circular References: If possible, redesign your object model to eliminate the circular references. This might involve introducing intermediary objects or changing navigation properties in Entity Framework.
Flatten Data Structure (Careful): In some cases, flattening the data structure (removing nested objects) might be appropriate, but use this approach cautiously as it can affect how you work with the data later.
Configuration Options (With Caution):

Ignore Cycles (System.Text.Json): This approach tells the serializer to ignore circular references during serialization. However, be aware that this can lead to incomplete or unexpected data on the receiving end. Use JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Ignore; for System.Text.Json.

Preserve References (System.Text.Json): This option allows serialization of cycles but can result in larger JSON payloads. Use JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; for System.Text.Json.

Ignore Loops (Newtonsoft.Json): If you're using the Newtonsoft.Json library, you can set SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; to ignore cycles. Similar to Ignore with System.Text.Json, exercise caution with this approach.

Choosing the Best Approach:

The ideal solution depends on your specific use case and data requirements. Here are some general guidelines:

If maintaining data structure integrity is crucial, prioritize refactoring your object model.
If omitting some data during serialization is acceptable, consider using Ignore options with caution.
Use Preserve options only if you understand the implications of larger payloads and potential data redundancy.
 */

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Path"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
