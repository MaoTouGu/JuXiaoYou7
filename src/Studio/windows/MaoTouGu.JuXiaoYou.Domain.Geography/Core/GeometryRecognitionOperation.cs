// ----------------------------------------------------------
//            文件：GeometryRecognitionOperation.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 14:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using MaoTouGu.JuXiaoYou.Domain.Geography.Services;

namespace MaoTouGu.JuXiaoYou.Domain.Geography.Core
{
    public sealed class GeometryRecognitionOperation(string _FileName, PageBase _Page, GeometryRecognitionCallback _Callback) : ObservableAsyncOperation
    {
        public override Task Run()
        {
            return Task.Run(async () =>
                            {
                                SetBusyText("读取数据……");
                                var buffer = await File.ReadAllBytesAsync(_FileName);
                                var r2     = await ImageToGeometryService.Recognize(buffer, _Page);

                                SetBusyText("正在解析边界……");
                                if (!r2.IsFinished)
                                {
                                    return;
                                }

                                var result        = r2.Value;
                                var literalString = NTSPointHelper.Write(result.Points);

                                //
                                // 写入
                                SetBusyText("解析边界成功，正在保存数据……");
                                
                                _Callback?.Invoke(_FileName,
                                                  literalString, 
                                                  (int)result.Width,
                                                  (int)result.Height, 
                                                  result.Geometry);
                                
                                
                                SetBusyText("解析边界成功，正在保存数据……");
                            });
        }
    }
}