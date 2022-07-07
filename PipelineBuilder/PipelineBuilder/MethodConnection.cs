namespace PipelineBuilder
{
    using System.Reflection;

    /// <summary>
    /// Represents the MethodConnection class.
    /// </summary>
    public class MethodConnection
    {
        /// <summary>
        /// Declare the sender method.
        /// </summary>
        private MethodInfo senderMethod;

        /// <summary>
        /// Declare the receiver method.
        /// </summary>
        private MethodInfo receiverMethod;

        /// <summary>
        /// Initialize a new instance of the <see cref="MethodConnection"/>.
        /// </summary>
        /// <param name="sender">Specified the sender.</param>
        /// <param name="receiver">Specfied the receiver.</param>
        public MethodConnection(MethodInfo sender, MethodInfo receiver)
        {
            this.senderMethod = sender;
            this.receiverMethod = receiver;
        }

        /// <summary>
        /// Gets the sender method.
        /// </summary>
        /// <value>The sender method.</value>
        public MethodInfo SenderMethod
        {
            get
            {
                return this.senderMethod;
            }
        }

        /// <summary>
        /// Gets the receiver method.
        /// </summary>
        /// <value>The receiver method.</value>
        public MethodInfo ReceiverMethod
        {
            get
            {
                return this.receiverMethod;
            }
        }
    }
}
