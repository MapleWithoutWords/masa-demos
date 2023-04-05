# Masa Framework源码解读-EventBus（可编排+拦截的事件总线）

## 序言

这篇文章我们来看下 ```Masa Framework``` 的事件总线。 ```Masa Framework``` 的事件总线不同于其它的事件总线框架， ```Masa Framework``` 的事件总线除了基本的发布订阅功能之外，它还支持**事件编排**和**事件拦截**，从而避免我们的流水账似的编程，降低系统维护难度，可以让我们的系统具备更高的扩展性。

## 简介

>  ```Masa Framework``` 的所有构建块（组件）都可以单独使用，也就是说我如果只想要 **事件总线** ，那么我只需要 **安装并引入** 事件总线的包就行了。

 ```Masa Framework``` 中的事件总线支持 **进程内事件** 和 **集成事件** ，比较有意思的是 ```Masa Framework``` 的事件总线支持 **事件编排** 和 **事件拦截** ，首先**事件编排**允许你**设置事件处理的执行顺序**，而**事件拦截**允许你发布事件的时候可以**拦截事件** 。

* **进程内事件**：也可以称为本地事件，进程内事件的发布者和订阅者在同一个进程内。它的使用场景：在同一个程序内各个模块之间相互通信。
* **集成事件**：集成事件就是事件的发布者和订阅者不在同一个进程内，是由过去行为产生的事件。也就是我们常说的跨进程跨服务通信。适用于不同进程或应用程序服务之间的通信。```Masa Framework``` 的集成支持推送到 **Dapr**

 ## 进程内事件案例

我们先来看下进程内事件，下面我在某个实际业务中使用 ```Masa Framework``` 进程内事件并结合 **事件编排** 的场景

* 场景：在一个业务系统中用户的权限信息很少会变动，为了避免每次获取或检查用户权限的时候都需要读取数据库，我们就需要给用户权限增加缓存。那么就会出现以下这种**流水账的代码逻辑**。每次操作完用户、角色、权限等信息都在原有逻辑上增加缓存，下次再增加其它操作例如授权日志的时候，又在后面追加。随着业务变得复杂，我们的代码也越来越复杂，经常出现一个方法 **500行以上的代码** ，甚至过之而不及（PS：我见过最多的是一个方法5000行，全是解析规则的逻辑）。这对于系统的 **后期维护以及扩展性极其不利** 。

```c#
public async Task CreateAsync(UserCreateDto dto)
{
    //新增用户以及用户权限、用户角色等

    //：添加用户缓存
    //：日志
    //：消息推送
}

public async Task UpdateAsync(UserUpdateDto dto)
{
    //修改用户以及用户权限、用户角色等

    //：更新用户缓存
    //：日志
    //：消息推送
}
```

针对以上场景，我们来看下```Masa Framework``` 的 **事件编排** 是怎么解决的。首先先对整个逻辑进行划分，然后将各个逻辑拆分成独立的业务块并进行编排，然后通过事件总线统一调度执行。如下图所示，使用 ```Masa Framework``` 的 **EventBus** 之后我们的逻辑可以支持横向扩展

![](https://img2023.cnblogs.com/blog/1525201/202304/1525201-20230405131856480-1116821357.jpg)

### 案例详解

基于上面 ```Masa Framework ```  **EventBus事件编排** 的设计，我们来看下具体项目中的落地是什么样的

> Demo项目下载地址
>
> 【GtHub】https://github.com/MapleWithoutWords/masa-demos
>
> 【Gitee】https://gitee.com/fengwuyan/masa-demos

1. 首先第一步：在项目中安装  ```Masa.Contrib.Dispatcher.Events``` Nuget包

![](https://img2023.cnblogs.com/blog/1525201/202304/1525201-20230405142433349-575015218.png)

2. 第二步：添加 **EventBus** 服务

```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//添加EventBus服务
builder.Services.AddEventBus();

var app = builder.Build();

app.MapControllers();
app.Run();
```

3. 第三步：定义一个用户更新的事件以及用户更新Dto：```UpdateUserEvent``` 和 ```UserUpdateDto``` 

   ![](https://img2023.cnblogs.com/blog/1525201/202304/1525201-20230405162257170-1505303224.png)

3. 我们在```UserController.cs```控制器中注入```IEventBus```对象（PS：创建UserController的过程我就不说了）

```c#
```







