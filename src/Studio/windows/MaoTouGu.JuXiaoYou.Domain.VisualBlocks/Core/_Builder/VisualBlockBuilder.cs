// ----------------------------------------------------------
//            文件：VisualBlockBuilder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 18:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Runtime.InteropServices;
using MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Models;
using NLog;

namespace MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Core
{
    public partial class VisualBlockBuilder
    {
        private static readonly Dictionary<string, IVisualBlockBuilder> _ByStringTypeMapper = new();
        private static readonly Dictionary<Type, IVisualBlockBuilder>   _ByTypeMapper       = new();
        private static readonly Dictionary<Type, DataTemplate>          _DataTemplateTable  = new();


        private static readonly ILogger _Logger = LoggerExt.GetLogger<VisualBlockBuilder>();



        public static void Scan(Assembly assembly)
        {
            if (assembly is null)
            {
                return;
            }

            var visualBlockTypes = assembly.GetTypes()
                                           .Where(IsVisualBlockType)
                                           .ToList();

            var needGuidAttributes = visualBlockTypes.Where(x => x.IsDefined(typeof(GuidAttribute)))
                                                     .ToList();

            foreach (var type in needGuidAttributes)
            {
                try
                {
                    var guid    = type.GetCustomAttribute<GuidAttribute>();
                    var obj     = (VisualBlock)Activator.CreateInstance(type);
                    var builder = (IVisualBlockBuilder)obj; // VisualBlock

                    if (guid is null || obj is null)
                    {
                        continue;
                    }

                    _ByStringTypeMapper.TryAdd(guid.Value, builder);
                    _ByTypeMapper.TryAdd(type, builder);

                    //
                    //
                    var itb      = builder.Build(obj);                              // VPO
                    var template = BuildTemplate(builder); // DataTemplate

                    //
                    //
                    _DataTemplateTable.TryAdd(itb.GetType(), template);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        static bool IsVisualBlockType(Type x)
        {
            return x.IsClass                             &&
                   !x.IsAbstract                         &&
                   x.IsAssignableTo(typeof(VisualBlock)) &&
                   x.IsAssignableTo(typeof(IVisualBlockBuilder));
        }

        public static VisualBlock GetVisualBlock(DataPart module)
        {
            if (module is null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(module.Type))
            {
                return null;
            }

            if (string.IsNullOrEmpty(module.JSON))
            {
                return null;
            }

            if (_ByStringTypeMapper.TryGetValue(module.Type, out var handler))
            {
                return handler.Build(module.JSON);
            }

            return null;
        }

        public static VisualBlockVPO GetVisualBlockVPO(VisualBlock block)
        {
            if (block is null)
            {
                return null;
            }

            if (_ByTypeMapper.TryGetValue(block.GetType(), out var handler))
            {
                return handler.Build(block);
            }

            return null;
        }

        public static DataTemplate BuildTemplate(IVisualBlockBuilder target)
        {
            var factory = new FrameworkElementFactory(target.GetTemplatedControlType());

            //
            // 设置DataContext为当前数据上下文
            factory.SetBinding(FrameworkElement.DataContextProperty, new Binding());

            var template = new DataTemplate
            {
                DataType   = target.GetType(),
                VisualTree = factory,
            };

            return template;
        }
    }
}