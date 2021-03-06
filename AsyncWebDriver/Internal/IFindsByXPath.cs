// Copyright (c) Oleg Zudov. All Rights Reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Zu.AsyncWebDriver.Internal
{
    /// <summary>
    ///     Defines the interface through which the user finds elements by XPath.
    /// </summary>
    public interface IFindsByXPath
    {
        /// <summary>
        ///     Finds the first element matching the specified XPath query.
        /// </summary>
        /// <param name="xpath">The XPath query to match.</param>
        /// <returns>The first <see cref="IWebElement" /> matching the criteria.</returns>
        Task<IWebElement> FindElementByXPath(string xpath,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        ///     Finds all elements matching the specified XPath query.
        /// </summary>
        /// <param name="xpath">The XPath query to match.</param>
        /// <returns>
        ///     A <see cref="ReadOnlyCollection{T}" /> containing all
        ///     <see cref="IWebElement">IWebElements</see> matching the criteria.
        /// </returns>
        Task<ReadOnlyCollection<IWebElement>> FindElementsByXPath(string xpath,
            CancellationToken cancellationToken = new CancellationToken());
    }
}