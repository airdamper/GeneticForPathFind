# GeneticForPathFind
使用[GeneticSharp](https://github.com/giacomelli/GeneticSharp)制作的寻路例子。

GeneticSharp 用起来非常简单，提供了非常丰富的接口，也可以自定义自己的变异和交叉方法。

**使用过程过程中遇到的两个问题：**

1. 编码和解的映射：这个需要自己设计，这决定求解的效率，如果设计的不好可能连解都求不出来，更不用说找到最优的解。
2. 适应度：如何设计适应度决定了种群的进化速度和方向。

最后就是编码方式，交叉方式，突变方式，这些 GeneticSharp 中提供了常见的方法也支持自定义。选择不同的方法对求解也有影响。

#### 参考：

1. 遗传基因算法介绍：https://www.jianshu.com/p/ae5157c26af9
2. 基于优先级编码：https://zhuanlan.zhihu.com/p/81749290
