# Masa Framework源码解读-MasaMinimalApi设计

## 序言
​		大家可能或多或少都了解过微软官方的 ```MinimalApi``` , 最开始刚出来那会我其实对 ```MinimalApi``` 是嗤之以鼻的，因为本身有Controller控制器能够明确定义请求方法出来，再者还有```Abp``` 的```动态webapi``` 珠玉在后。但是当我看了下**Masa的MinimalApi**之后，又改变了我对它的看法。

​		这里举一个实际的场景：在我们项目开发中很多的控制器都只是转发了服务层的代码，从而造成了大量“重复代码”，也带来了一些重复的工作。例如：

```c#
public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userAppService) => _userService = userService;
    [HttpGet]
    public async Task<PageResult<List<xxxDto>>> GetListAsync(xxxDto input)
    {
        //控制器层什么都没有干，只是为了转发下业务层
        return await _userAppService.GetListAsync(input);
    }
    //省略……
}
```

​		控制器只是做转发，其实啥都没干。举个例子：假设我们的系统中有几百张表，那么我基本就得建几百个控制器，但是这些控制器都没啥意义，因为它啥都没干。

## 简介
​		那么针对上诉问题，我们来看下Masa Framework的**MasaMinimalApi** 怎么解决的。首先**Masa的Minimal Api**是在微软官方Minimal Api的基础上扩展的，它不同于官方的minimalApi，但是底层是调用的官方的minimalapi接口。并且使用 ```MasaMinimalApi``` 可以达到跟```Abp的动态webapi```一样的效果，他比动态webapi的性能更高，也更加简洁轻量。最主要的是**MASA Framework**的**Minimal Api组件**可以单独使用，不像```Abp```这个“老流氓”一样：使用任何一个组件都必须用整个abp框架。

## MasaMinimalApi 使用

> Demo项目下载地址：https://github.com/MapleWithoutWords/masa-demos 或者 https://gitee.com/fengwuyan/masa-demos

​		我们先来看看 **MasaMinimalApi**怎么使用，在这里我以一个简单的项目示例，下图为项目的结构。

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230320004145762-1104211802.png)

1. 第一步：在我们的Service层添加```Masa.Contrib.Service.MinimalAPIs``` NuGet包，或者使用命令安装

```c#
dotnet add package Masa.Contrib.Service.MinimalAPIs
```

2. 第二步：在Program.cs文件中添加以下代码

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319144441006-2032861340.png)

3. 第三步：让Service层中的代码继承 ```ServiceBase``` 类

```c#
public class UserService : ServiceBase, IUserService
{
    public async Task<string> GetUserNameAsync()
    {
        return "userName";
    }
}
```

4. 最终效果：

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319144737615-1073429980.png)



## Masa MinimalApi原理

​		那么```Masa MinimalApi``` 是怎么做到动态的根据服务层方法映射成控制器接口呢。那么接下来我们来看下```Masa MinimalApi``` 内部是怎么实现的。下面进入我们的源码环节。下图为整个minimalapi的实现流程图

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319221828946-1531151428.png)

​		首先MasaFramework中的minimalApi使用是需要在程序中调用这两个方法：AddMasaMinimalAPIs和MapMasaMinimalAPIs。这两个方法联合起来的整体流程：

1. 先从程序集中找到所有继承自ServiceBase的子类

2. 将这些子类添加到IServiceCollection容器中

3. 遍历获取每个类中的方法，然后将方法封装成一个委托，并根据规则获取路由及http请求方法，

4. 最后调用微软官方的minimalApi方法MapMethods（这个方法最终也是转换成endpoint）。下面让我们来看下具体源码吧

### 1. ```AddMasaMinimalAPIs```方法内部

> 我们来看看AddMasaMinimalAPIs方法内部做了那些事

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319202249989-1552875389.png)

```AddMasaMinimalAPIs``` 方法主要是从程序集中找到所有的服务类并添加到容器中，以及配置全局路由规则的

1. 添加全局路由配置
2. 从程序集中找到所有继承自```ServiceBase```的类
3. 把这些子类添加到容器中,以```GlobalMinimalApiOptions```静态类中

### 2. ```MapMasaMinimalAPIs``` 方法内部

> 再看看MapMasaMinimalAPIs方法内部的做了那些操作

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319214749781-1419092105.png)

而```MapMasaMinimalAPIs``` 则是将上诉方法找到的服务类，根据一定约定及配置，获取Service类中的方法对应的路由等，并且将方法组装成一个委托，然后调用官方的MapMethods方法。

1. 从遍历继承自```ServiceBase``` 的类，获取全局路由配置
2. 调用```ServiceBase``` 类的AutoMapRoute方法
   1. 获取当前类中的方法
   2. 遍历得到的方法，组装一个委托，调用微软官方的```MapMethods``` 

## 总结

1. Masa的MinimalApi最终还是调用微软官方的MapMethods方法映射成endpoint，只是在这上面加了一层ServiceBase。

2. MasaMinimalApi能够减少我们程序中重复的代码，以及重复工作（PS：建控制器和转发业务层代码）