using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    // 活动时间到了之后调用的委托
    public delegate void Delegate_TimeOut();
    // 时间管理器
    class TimeMgr
    {
        // TimeConfigID ： 时间参数配置表的id
        // Delegate ： 委托
        // 活动管理器中调用,注册时间参数，让时间管理器管理时间相关逻辑，当时间到了，进行通知
        public void Register(int TimeConfigID, Delegate_TimeOut Delegate)
        {
            m_Delegate_TimeOut = Delegate;
        }

        public void CheckTimeOut()
        {
            // 通过逻辑检测时间是否到活动开始时间
            // 如果到了活动开始时间
            // 调用委托，触发活动开始逻辑
            m_Delegate_TimeOut();
        }
        private Delegate_TimeOut m_Delegate_TimeOut;
    }

    // 活动的具体行为委托
    public delegate void Delegate_ActivityDo();

    // 活动管理器
    class ActivityMgr
    {
        // 初始化操作
        ActivityMgr()
        {
            // 活动管理器初始化时，注册相应的时间参数到时间管理器,由时间管理器管理时间逻辑，活动本身不关心时间
            m_TimeMgr.Register(1, ActivityOpen);
        }

        // 单例
        public static ActivityMgr GetInstance()
        {
            if (m_ActivityMgr != null)
                return m_ActivityMgr;
            m_ActivityMgr = new ActivityMgr();
            return m_ActivityMgr;
        }

        // 注册给时间管理器的事件，当活动开启时，该函数被调用
        void ActivityOpen()
        {
            // 活动开启的一些初始化操作

            // 具体的活动行为
            m_Delegate_ActivityDo();
        }

        // 具体活动中调用，将自己的活动行为注册给活动管理器
        public void Register(Delegate_ActivityDo Delegate)
        {
            m_Delegate_ActivityDo += Delegate;
        }

        private static ActivityMgr m_ActivityMgr;
        private TimeMgr m_TimeMgr;
        Delegate_ActivityDo m_Delegate_ActivityDo;
    }


    // 具体活动 - 应援大作战
    class Activity_Yingyuandazuozhan
    {
        // 活动的初始化操作
        public Activity_Yingyuandazuozhan()
        {
            ActivityMgr.GetInstance().Register(ActivityDo);
        }

        // 应援大作战的具体逻辑
        void ActivityDo()
        {

        }
    }

    // 具体活动 - 泡面活动
    class Activity_Paomian
    {
        // 活动的初始化操作
        public Activity_Paomian()
        {
            ActivityMgr.GetInstance().Register(ActivityDo);
        }

        // 泡面活动的具体逻辑
        void ActivityDo()
        {

        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Activity_Yingyuandazuozhan Activity1 = new Activity_Yingyuandazuozhan();
            Activity_Paomian Activity_2 = new Activity_Paomian();
        }
    }
}


/*
流程梳理： 具体活动类在创建对象实例的时候，就会将自己活动的具体行为注册给活动管理器。
而活动管理器在构造函数中，也将活动时间开始的事件注册给时间管理器。
时间管理器是小付写的，活动管理器是老黄写的，具体活动可能是大庆或书顺等人。这时新添加一个活动，开发人员只需要在自己的活动类中
调用老黄写的活动管理器的Register方法，就可以通过委托的方式将自己活动的具体行为注册给活动管理器，而老黄不需要改动任何代码

     
*/
