using System;
using System.Runtime.Serialization;

namespace MahApps.Metro.IconPacks.Browser
{
    [Serializable]
    public class BusyToken : WeakReference, IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="BusyToken"/> is disposing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposing; otherwise, <c>false</c>.
        /// </value>
        public bool Disposing { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusyToken"/> class.
        /// </summary>
        /// <param name="stack">The stack.</param>
        public BusyToken(BusyStack stack) : base(stack)
        {
            stack.Push(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposing)
            {
                Disposing = true;

                if (Target != null)
                {
                    var stack = Target as BusyStack;
                    stack?.Pull();
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected BusyToken(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context); // TODO
        }
    }
}
