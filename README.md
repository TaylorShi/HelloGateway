## 什么是BFF

用于前端的后端模式(Backend For Frontend，BFF)，它负责认证授权、负责服务聚合，目标是为前端提供服务。

![](/Assets/2022-10-23-22-44-04.png)

### 作者介绍

> 创建独立的后端服务，由特定的前端应用或接口来消费。当你想避免为多个接口定制一个后端时，这种模式很有用。这种模式最早是由Sam Newman描述的。

**随着网络的出现和成功，提供用户界面的事实方式已经从厚重的客户端应用程序转移到了通过网络提供的界面，这一趋势也使得基于SAAS的解决方案普遍得到了发展**。通过网络提供用户界面的好处是巨大的——**主要是发布新功能的成本大大降低，因为客户端的安装成本(在大多数情况下)被完全消除了**。

但这个简单的世界并没有持续多久，因为不久之后就进入了移动时代。现在我们遇到了一个问题。我们有服务器端的功能，我们希望通过我们的桌面Web UI和一个或多个移动UI来展示。**对于一个最初以桌面网络用户界面为基础开发的系统，我们在适应这些新类型的用户界面时经常会遇到问题，因为我们已经在桌面网络用户界面和我们的支持服务之间建立了紧密的联系**。

我在REA和SoundCloud看到的解决这个问题的方法是，**不要有一个通用的API后端，而是每个用户体验都有一个后端**——或者像(前SoundClouder)Phil Calçado所说的那样，一个前端后端(BFF)。**从概念上讲，你应该把面向用户的应用程序看成是两个组件**——一个生活在你周围的客户端应用程序，和一个在你周围的服务器端组件(BFF)。

**BFF与特定的用户体验紧密相连，并且通常由与用户界面相同的团队维护，从而更容易根据用户界面的要求定义和调整API，同时也简化了客户端和服务器组件的发布过程。**

BFF紧密地集中在一个单一的用户界面上，而且只是那个用户界面。这使它能够集中精力，因此也会更小。

**对于一个只提供网页用户界面的应用程序来说，我认为只有当你在服务器端有大量的聚合需求时，BFF才有意义**。否则，我认为其他的UI组合技术也可以很好地工作，而不需要额外的服务器端组件(我希望很快会讨论这些问题)。

**不过，当你需要为移动UI或第三方提供特定的功能时，我会强烈考虑从一开始就为每一方使用一个BFFs**。如果部署额外服务的成本很高，我可能会重新考虑，**但BFF所带来的分离关注，在大多数情况下是一个相当有说服力的提议**。如果构建用户界面的人和下游服务之间有明显的分离，我甚至会更倾向于使用BFF，原因如上所述。

## 相关文章

* [乘风破浪，遇见最佳跨平台跨终端框架.Net Core/.Net生态 - 浅析ASP.NET Core网关和BFF，使用Ocelot打造面向移动和桌面专属服务](https://www.cnblogs.com/taylorshi/p/16818182.html)