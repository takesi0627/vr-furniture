using UnityEngine;

namespace hekira
{
    public class State<T>
    {
        // このステートを利用するインスタンス
        protected T owner;

        Object[] argList;
        public Object[] ParameterList {
            get { return argList; }
            set { argList = value; }
        }

        public State(T owner)
        {
            this.owner = owner;
        }

        // このステートに遷移する時に一度だけ呼ばれる
        public virtual void Enter() { }

        // このステートである間、毎フレーム呼ばれる
        public virtual void Execute() { }

        // このステートから他のステートに遷移するときに一度だけ呼ばれる
        public virtual void Exit() { }
    }
}