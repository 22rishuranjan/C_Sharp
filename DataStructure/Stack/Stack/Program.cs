using System;
using System.Collections;
using System.Collections.Generic;

namespace myStack
{
    class Program
    {
        static void Main(string[] args)
        {
           //Stack<int> myStack = new Stack<int>();

            Stack myStack = new Stack();

            var arr = new int[]{ 1, 2, 3, 4, 5 };


            //myStack = (Stack)arr;

        }

        public void PushItems(Stack myStack,object[] arr)
        {
            foreach (var item in arr)
            {
                myStack.Push(item);
            }

        }

        public object PopItems(Stack myStack)
        {
           return myStack.Pop();
        }

        public object PeekItems(Stack myStack)
        {
            return myStack.Peek();
        }
    }
}
