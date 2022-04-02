# Powers.EzHttpClient

�ṩ�˶�HttpClient�ķ�װ, �Լ����õ�`get`,`post`,`put`,`delete`����.

get:
```csharp
var res = await HttpClientWrapper
    .Create()
    .Url("http://localhost:5299/WeatherForecast")
    .Authentication("Bearer", token)
    .GetAsync<IEnumerable<WeatherForecast>>();
```

post:
```csharp
var res = await HttpClientWrapper
    .Create()
    .Url("http://localhost:5299/WeatherForecast")
    .Authentication("Bearer", token)
    .Body(new { })
    .PostAsync<ReturnDto>();
```

put:
```csharp
var res = await HttpClientWrapper
    .Create()
    .Url("http://localhost:5299/WeatherForecast")
    .Authentication("Bearer", token)
    .Body(new { })
    .PutAsync<ReturnDto>();
```

delete:
```csharp
var res = await HttpClientWrapper
    .Create()
    .Url("http://localhost:5299/WeatherForecast")
    .Authentication("Bearer", token)
    .DeleteAsync<ReturnDto>();
```