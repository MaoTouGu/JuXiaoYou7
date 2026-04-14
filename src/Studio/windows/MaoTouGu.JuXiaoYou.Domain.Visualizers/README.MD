# Reformat Metadata And Visualizer

设定的值如何reformat成具体的视觉效果？

一、使用MonikerSettingMonitor行为，订阅设定的变动

二、直接生成Binding，绑定在对应的控件上。

这两种方法对应着两套不同的需要解决的问题。

## 一、使用MonikerSettingMonitor行为，订阅设定的变动

## 二、直接生成Binding，绑定在对应的控件上。

### 解决控件移除时PropertyChanged事件的

通常我们要解决事件与回调函数之间的强引用，最好用的就是弱事件模式。

但是在这个需求下，弱事件的坏处是无法准确的推流数据上下文，而且是3个。



所以我就打算用Behavior来解决。