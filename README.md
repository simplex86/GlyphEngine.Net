# GlyphEngine

基于 .Net 和字符渲染的控制台游戏引擎。

![](./Docs/images/snake_game.webp)

## 启动

``` csharp
var engine = new GlyphEngine.GlyphEngine("GlyphEngine Startup");
engine.Start();
```

## 程序入口

引擎启动后，会自动寻找并加载自定义的程序入口。只需满足以下两个条件

- 实现 IEngineEntry 接口
- 带有 CEngineEntryAttribute 特性标签

``` csharp
[CEngineEntry]
public class AppEntry : IEngineEntry
{
    public async Task Start()
    {
        // Your Codes Here
        await Task.CompletedTask;
    }

    public void Update(float dt)
    {
        // Your Codes Here
    }

    public void Exit()
    {
        // Your Codes Here
    }
}
```