// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    消息显示类 继承自异常类，利用异常全局捕获，已达到提示消息的目的
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================
using System;

namespace  JpFramework.Tools
{
    public class MessageTipShow:Exception
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgTypes { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 显示时长，只有当MsgTypes=MsgType.Normal时有作用
        /// </summary>
        public int Time { get; set; }

        public MessageTipShow(string value)
        {
            MsgTypes = MsgType.Warning;
            Message = value;
        }

        public MessageTipShow(string value,MsgType type,int time=1000)
        {
            Message = value;
            MsgTypes = type;
            Time = time;
        }
    }

    public enum MsgType
    {
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 良好信息
        /// </summary>
        Ok,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error,
        /// <summary>
        /// 普通提示
        /// </summary>
        Normal,
        
        /// <summary>
        /// 窗口提示
        /// </summary>
        Form

        

    }
}
