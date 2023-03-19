# Masa Framework源码解读-MasaMinimalApi设计

## 序言

​		相信大家可能或多或少都了解过微软官方的 ```MinimalApi``` , 最开始刚出来那会我其实对 ```MinimalApi``` 是嗤之以鼻的，因为本身有Controller控制器能够明确定义请求方法出来，再者还有```Abp``` 的```动态webapi``` 珠玉在后。但是当我看了下masa的minimalApi之后，又改变了我对它的看法。

## 简介

​		Masa的Minimal Api是基于微软官方MinimalApi的基础上扩展的，使用 ```MasaMinimalApi``` 可以达到跟```Abp的动态webapi```一样的效果，并且性能更高，更加简洁轻量。比较好的是MASA Framework的MinimalApi组件都可以单独使用，不像```Abp```这个“老流氓”一样，必须用整个abp框架才能用它的功能（PS：我以前是Abp的重度使用者，用了Masa之后就回不去了。）。

## MasaMinimalApi 示例

> Demo项目下载地址：https://github.com/MapleWithoutWords/masa-demos 或者 https://gitee.com/fengwuyan/masa-demos

​		下面我将拿出我在小型项目中使用MasaMinimalApi的案例展现给大家。因为我们实际项目中很多的控制器都只是转发了服务层的代码，从而造成了大量“重复代码”，也带来了一些重复的工作。例如：

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

​		控制器只是做转发，其实啥都没干。假设我们的轻量级```MOM平台```有几百张表，那么我基本就得建几百个控制器，但是这些控制器都没啥意义，为了不再做重复的事情，我们引入了MasaMinimalApi。让我们一起来看下：

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

​		我们使用```Masa MinimalApi```最终也能达到```Abp动态api```的效果，而且性能更快，只需要引入一个包就行了。接下来我们来看下```Masa MinimalApi``` 内部是怎么实现的。下面进入我们的源码环节。

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319221828946-1531151428.png)

​		MasaFramework中的minimalApi，会先从程序集中找到所有继承自ServiceBase的子类，遍历获取每个类中的方法，然后将方法封装成一个委托，并根据规则获取路由及http请求方法，最后调用微软官方的minimalApi方法MapMethods（这个方法最终也是转换成endpoint）。下面让我们来看下具体源码吧

### 1. ```AddMasaMinimalAPIs```方法内部

> AddMasaMinimalAPIs方法主要做三件事

1. 添加全局路由配置
2. 从程序集中找到所有继承自```ServiceBase```的类
3. 把这些子类添加到容器中,以```GlobalMinimalApiOptions```静态类中

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319202249989-1552875389.png)



### 2. ```MapMasaMinimalAPIs``` 方法内部

> 而MapMasaMinimalAPIs方法内部主要是

1. 从遍历继承自```ServiceBase``` 的类，获取全局路由配置
2. 调用```ServiceBase``` 类的AutoMapRoute方法
   1. 获取当前类中的方法
   2. 遍历得到的方法，组装一个委托，调用微软官方的```MapMethods``` 

![](https://img2023.cnblogs.com/blog/1525201/202303/1525201-20230319214749781-1419092105.png)



## 总结

1. Masa的MinimalApi最终还是调用微软官方的MapMethods方法映射成endpoint，只是在这上面加了一层ServiceBase。

2. MasaMinimalApi能够减少我们程序中重复的代码，以及重复工作（PS：建控制器和转发业务层代码）