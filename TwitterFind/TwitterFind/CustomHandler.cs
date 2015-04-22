using Leap;
using System;

namespace TwitterFind
{
    /// <summary>
    /// 
    /// </summary>
    class CustomHandler : IDisposable
    {


        private readonly Controller _myController;
        private bool _disposed;



        public CustomHandler()
        {
            Listener = new CustomLeapListener();
            _myController = new Controller();
            _myController.AddListener(Listener);

            CustomLeapListener tempListener = null;
            Controller tempController = null;
            try
            {
                tempListener = new CustomLeapListener();
                tempController = new Controller();
                tempController.AddListener(tempListener);
                //Now we are safe from exceptions.
                Listener = tempListener;
                _myController = tempController;
                tempListener = null;
                tempController = null;
            }
            finally
            {
                if (tempListener != null) tempListener.Dispose();
                if (tempController != null) tempController.Dispose();
            }

        }

        ~CustomHandler()
        {
            Dispose(false);
        }
        public CustomLeapListener Listener { get; set; }





        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _myController.RemoveListener(Listener);
                _myController.Dispose();
                Listener.Dispose();
            }
            _disposed = true;
        }


    }
}
