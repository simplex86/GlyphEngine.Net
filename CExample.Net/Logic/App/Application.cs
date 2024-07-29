using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleX.CEngine;

namespace CExample
{
    [ApplicationEntry]
    internal class Application : IApplication
    {
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            CSceneManager.Load<SnakeScene>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {

        }
    }
}
