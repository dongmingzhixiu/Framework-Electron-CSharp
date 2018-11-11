// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    定义统一的 实体类方法，方法必须具备表名称
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

namespace  JpFramework
{
    public interface IEntity
    {
       /// <summary>
       /// 设置表名称 ，通过反射 修改数据 新增数据时需要
       /// </summary>
       /// <returns></returns>
        string TableName();
    }

}
