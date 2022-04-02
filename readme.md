# Powers.EzHttpClient

提供了对HttpClient的封装, 以及常用的`get`,`post`,`put`,`delete`方法.

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