// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :     配置文件 工具类
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================
using System.Configuration;

namespace  JpFramework.Tools
{
    /// <summary>
    ///     配置文件 工具类
    /// </summary>
    public class ConfigTools
    {
        /// <summary>
        ///     根据 key 得到 AppString节点的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}